using System.Collections.Generic;
using UnityEngine;

namespace Core.GamePlay.Support
{
    public class DetectZipPointOnObject : MonoBehaviour
    {
        [SerializeField] private Transform _centrePoint;
        [SerializeField] private List<Edge> _edges;
        public Vector3 GetZipPointByRayCast(Camera camera)
        {
            var direction = _centrePoint.position - camera.transform.position;
            var ray = new Ray(camera.transform.position, direction);
            if (Physics.Raycast(ray, out var hit))
            {

            }
            return Vector3.negativeInfinity;
        }

        private Vector3 GetZipPointOnEdge(Camera camera, Edge edge)
        {
            var startPoint = edge.StartPoint.position;
            var endPoint = edge.EndPoint.position;
            Ray ray = new Ray(startPoint, endPoint - startPoint);
            Plane plane = new Plane(camera.transform.forward, camera.transform.position);
            if (plane.Raycast(ray, out var distance))
            {
                var point = ray.GetPoint(distance);
                var viewportPoint = camera.WorldToViewportPoint(point);
                if (viewportPoint.x >= 0 && viewportPoint.x <= 1 && viewportPoint.y >= 0 && viewportPoint.y <= 1 && viewportPoint.z >= 0)
                {
                    return point;
                }
            }
            return ProjectPointOnLineSegment(camera.transform.position, startPoint, endPoint);
        }

        public Vector3 ProjectPointOnLineSegment(Vector3 point, Vector3 lineStart, Vector3 lineEnd)
        {
            Vector3 lineDirection = lineEnd - lineStart;
            Vector3 pointDirection = point - lineStart;

            float lineLength = lineDirection.magnitude;
            lineDirection.Normalize();

            float projectionLength = Vector3.Dot(pointDirection, lineDirection);
            projectionLength = Mathf.Clamp(projectionLength, 0, lineLength);

            return lineStart + lineDirection * projectionLength;
        }
    }
}