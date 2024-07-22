using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

namespace Core.GamePlay.Support
{

    public class EdgeBuilding
    {
        public Vector3 StartPoint;
        public Vector3 EndPoint;

        public EdgeBuilding(Vector3 startPoint, Vector3 endPoint)
        {
            StartPoint = startPoint;
            EndPoint = endPoint;
        }

        public (Vector3, float) GetClosestPointWithCameraOnEdge(Camera camera)
        {
            var startPointInViewport = camera.WorldToViewportPoint(StartPoint);
            var endPointInViewport = camera.WorldToViewportPoint(EndPoint);

            if (!(startPointInViewport.x >= 0 && startPointInViewport.x <= 1 && startPointInViewport.y >= 0 && startPointInViewport.y <= 1) &&
                !(endPointInViewport.x >= 0 && endPointInViewport.x <= 1 && endPointInViewport.y >= 0 && endPointInViewport.y <= 1))
            {
                return (Vector3.negativeInfinity, float.MaxValue);
            }

            float tx = Mathf.Clamp01((0.5f - startPointInViewport.x) / (endPointInViewport.x - startPointInViewport.x));
            float ty = Mathf.Clamp01((0.5f - startPointInViewport.y) / (endPointInViewport.y - startPointInViewport.y));

            Vector3 closestPointInWorldFlowX = Vector3.Lerp(StartPoint, EndPoint, tx);
            Vector3 closestPointInWorldFlowY = Vector3.Lerp(StartPoint, EndPoint, ty);

            var distanceX = Vector3.Distance(camera.transform.position, closestPointInWorldFlowX);
            var distanceY = Vector3.Distance(camera.transform.position, closestPointInWorldFlowY);

            if (distanceX < distanceY)
            {
                return (closestPointInWorldFlowX, distanceX);
            }
            else
            {
                return (closestPointInWorldFlowY, distanceY);
            }
        }
    }

    public struct EdgebuildingComparer : IComparer<EdgeBuilding>
    {
        private EdgeBuilding _start;
        private EdgeBuilding _end;
        private Camera _camera;

        public int Compare(EdgeBuilding x, EdgeBuilding y)
        {
            var (pointX, distanceX) = x.GetClosestPointWithCameraOnEdge(_camera);
            var (pointY, distanceY) = y.GetClosestPointWithCameraOnEdge(_camera);
            if (distanceX < distanceY)
            {
                return -1;
            }
            else if (distanceX > distanceY)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }

    public class ZipPointOnObject : MonoBehaviour
    {
        [SerializeField] private MeshFilter _mesh;
        [SerializeField] private int _numberOfVertices;
        [SerializeField] private bool _isCustomZipPoint;

        private List<EdgeBuilding> _edges;

        private void Awake()
        {
            Initial();
            foreach (var edge in _edges)
            {
                Debug.DrawLine(edge.StartPoint, edge.EndPoint, Color.red, 1000);
            }
        }

        private void Initial()
        {
            _mesh = GetComponent<MeshFilter>();
            _edges = new List<EdgeBuilding>();
            var tmpVertices = new List<Vector3>();
            var vertices = _mesh.mesh.vertices.ToList();
            vertices.Sort((x, y) =>
            {
                if (x.y < y.y)
                {
                    return 1;
                }
                else if (x.y > y.y)
                {
                    return -1;
                }
                else
                {
                    return 0;
                }
            });
            var y_max = vertices[0].y;
            int i = 1;
            var point = vertices[i];
            i++;
            tmpVertices.Add(vertices[0]);
            while (point.y == y_max)
            {
                if (!tmpVertices.Contains(point))
                {
                    tmpVertices.Add(point);
                }

                point = vertices[i++];
            }

            if (tmpVertices.Count < 2)
            {
                _edges.Add(new EdgeBuilding(transform.TransformPoint(tmpVertices[0]), transform.TransformPoint(tmpVertices[0])));
                return;
            }

            // Calculate the centroid of the polygon
            // Vector3 centroid = new Vector3(
            //     tmpVertices.Average(v => v.x),
            //     tmpVertices.Average(v => v.y),
            //     tmpVertices.Average(v => v.z)
            // );

            // Sort the vertices by the angle between the centroid and the vertex
            // tmpVertices.Sort((v1, v2) =>
            // {
            //     float angle1 = Mathf.Atan2(v1.y - centroid.y, v1.x - centroid.x);
            //     float angle2 = Mathf.Atan2(v2.y - centroid.y, v2.x - centroid.x);
            //     return angle1.CompareTo(angle2);
            // });

            tmpVertices = FindConvexHull(tmpVertices);

            _edges = new List<EdgeBuilding>();
            for (i = 0; i < tmpVertices.Count - 1; i++)
            {
                _edges.Add(new EdgeBuilding(transform.TransformPoint(tmpVertices[i]), transform.TransformPoint(tmpVertices[i + 1])));
            }

            _edges.Add(new EdgeBuilding(transform.TransformPoint(tmpVertices[tmpVertices.Count - 1]), transform.TransformPoint(tmpVertices[0]))); // Connect the last vertex with the first one
        }

        public (Vector3, float) GetZipPoint(Transform player, Camera camera)
        {
            var distance = float.MaxValue;
            var closetstPoint = Vector3.negativeInfinity;
            bool isHavePoint = false;
            Debug.Log("GetZipPoint " + transform.name);
            foreach (var edge in _edges)
            {
                var (point, dist) = GetClosestPointWithCameraOnEdge(edge, camera);
                if (point != Vector3.negativeInfinity)
                {
                    isHavePoint = true;
                    if (dist < distance)
                    {
                        distance = dist;
                        closetstPoint = point;
                    }
                }
            }

            if (isHavePoint)
            {
                return (closetstPoint, distance);
            }
            else
            {
                return (Vector3.negativeInfinity, float.MaxValue);
            }
        }

        private (Vector3, float) GetClosestPointWithCameraOnEdge(EdgeBuilding edge, Camera camera)
        {
            return edge.GetClosestPointWithCameraOnEdge(camera);
        }

        private List<Vector3> FindConvexHull(List<Vector3> vertices)
        {
            if (vertices.Count < 3) return vertices;
        
            // Tìm điểm bắt đầu
            Vector3 startVertex = vertices.OrderBy(v => v.x).First();
            vertices.Remove(startVertex);
        
            // Sắp xếp các điểm còn lại dựa vào góc
            vertices.Sort((a, b) => Vector3.SignedAngle(Vector3.right, a - startVertex, Vector3.up).CompareTo(Vector3.SignedAngle(Vector3.right, b - startVertex, Vector3.up)));
        
            // Thêm điểm bắt đầu vào danh sách để đóng vòng lặp
            vertices.Add(startVertex);
        
            Stack<Vector3> hull = new Stack<Vector3>();
            hull.Push(startVertex);
            hull.Push(vertices[0]);
        
            for (int i = 1; i < vertices.Count; i++)
            {
                Vector3 top = hull.Pop();
                while (hull.Count > 0 && Vector3.Cross(vertices[i] - hull.Peek(), top - hull.Peek()).y >= 0)
                {
                    top = hull.Pop();
                }
                hull.Push(top);
                hull.Push(vertices[i]);
            }

            return hull.ToList();
        }
    }
}