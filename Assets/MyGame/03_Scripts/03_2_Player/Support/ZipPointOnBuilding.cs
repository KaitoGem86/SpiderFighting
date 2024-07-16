using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.GamePlay.Support
{
    [Serializable]
    public class Edge
    {
        public Transform StartPoint;
        public Transform EndPoint;
    }
    public class ZipPointOnBuilding : MonoBehaviour
    {
        [SerializeField] private List<Edge> _edges;

        public Vector3 GetZipPoint(Transform player)
        {
            var minDistance = float.MaxValue;
            Edge minEdge = null;
            foreach (var edge in _edges)
            {
                var distance = GetDistanceToEdge(player, edge);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    minEdge = edge;
                }
            }
            return GetClosestPointOnEdge(player, minEdge);
        }

        private float GetDistanceToEdge(Transform player, Edge edge)
        {
            Vector3 v = edge.EndPoint.position - edge.StartPoint.position;
            Vector3 w = player.position - edge.StartPoint.position;

            float c1 = Vector3.Dot(w, v);
            if (c1 <= 0)
                return Vector3.Distance(player.position, edge.StartPoint.position);

            float c2 = Vector3.Dot(v, v);
            if (c2 <= c1)
                return Vector3.Distance(player.position, edge.EndPoint.position);

            float b = c1 / c2;
            Vector3 Pb = edge.StartPoint.position + b * v;

            return Vector3.Distance(player.position, Pb);
        }

        public Vector3 GetClosestPointOnEdge(Transform player, Edge edge)
        {
            Vector3 v = edge.EndPoint.position - edge.StartPoint.position;
            Vector3 w = player.position - edge.StartPoint.position;

            float c1 = Vector3.Dot(w, v);
            if (c1 <= 0)
                return edge.StartPoint.position;

            float c2 = Vector3.Dot(v, v);
            if (c2 <= c1)
                return edge.EndPoint.position;

            float b = c1 / c2;
            Vector3 Pb = edge.StartPoint.position + b * v;

            return Pb;
        }
    }
}