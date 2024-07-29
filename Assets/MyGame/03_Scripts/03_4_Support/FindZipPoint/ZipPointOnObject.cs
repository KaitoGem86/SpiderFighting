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
            Vector3 viewPortCenter = new Vector3(0.5f, 0.5f, 0.5f);
            Vector3 worldCenter = camera.ViewportToWorldPoint(viewPortCenter);
            Vector3 forward = worldCenter - camera.transform.position;
            Vector3 normal = Vector3.Cross(forward, Vector3.up);
            Plane plane = new Plane(normal, worldCenter);

            if (TryGetIntersection(StartPoint, EndPoint, plane, out Vector3 intersection))
            {
                var tmp = camera.WorldToViewportPoint(intersection);
                tmp.y = 0;
                return (intersection, Vector3.Distance(tmp, new Vector3(0.5f, 0.5f, 0)));
            }
            else
            {
                Vector3 closestPoint = GetClosestEndPointToPlane(StartPoint, EndPoint, plane);
                return (closestPoint, Vector3.Distance(closestPoint, camera.transform.position));
            }
        }

        bool TryGetIntersection(Vector3 start, Vector3 end, Plane plane, out Vector3 intersection)
        {
            Ray ray = new Ray(start, end - start);
            float enter;
            if (plane.Raycast(ray, out enter) && enter <= Vector3.Distance(start, end))
            {
                intersection = ray.origin + ray.direction * enter;
                return true;
            }

            intersection = Vector3.zero;
            return false;
        }

        Vector3 GetClosestEndPointToPlane(Vector3 start, Vector3 end, Plane plane)
        {
            float distanceToStart = Mathf.Abs(plane.GetDistanceToPoint(start));
            float distanceToEnd = Mathf.Abs(plane.GetDistanceToPoint(end));

            return distanceToStart < distanceToEnd ? start : end;
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
        [SerializeField] private List<GameObject> _display;
        [SerializeField] private int _numberOfVertices;
        [SerializeField] private bool _isCustomZipPoint;

        private List<EdgeBuilding> _edges;
        private List<(Vector3, float)> _closestPoints;

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

            tmpVertices = FindConvexHull(tmpVertices);

            _edges = new List<EdgeBuilding>();
            for (i = 0; i < tmpVertices.Count - 1; i++)
            {
                _edges.Add(new EdgeBuilding(transform.TransformPoint(tmpVertices[i]), transform.TransformPoint(tmpVertices[i + 1])));
            }

            _edges.Add(new EdgeBuilding(transform.TransformPoint(tmpVertices[tmpVertices.Count - 1]), transform.TransformPoint(tmpVertices[0]))); // Connect the last vertex with the first one
        }

        public List<(Vector3, float)> GetZipPoint(Camera camera)
        {
            _closestPoints ??= new List<(Vector3, float)>();
            _closestPoints.Clear();
            foreach (var edge in _edges)
            {
                var (point, distance) = GetClosestPointWithCameraOnEdge(edge, camera);
                _closestPoints.Add((point, distance));
            }
            return _closestPoints;
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