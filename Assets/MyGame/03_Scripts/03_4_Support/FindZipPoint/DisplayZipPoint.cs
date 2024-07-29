using DG.Tweening;
using UnityEngine;

namespace Core.GamePlay.Support
{
    public class DisplayZipPoint : MonoBehaviour
    {
        [SerializeField] private RectTransform _displayZipPoint;
        private GameObject ObjectToZip { get; set; }
        public Vector3 TargetZipPoint;
        private Vector3 EndZipPoint
        {
            get
            {
                Debug.DrawRay(TargetZipPoint + Vector3.up , Vector3.down * 100, Color.red, 5f);
                Debug.DrawRay(TargetZipPoint + Vector3.up * 0.05f, Vector3.down * 100, Color.red, 5f);
                if (Physics.Raycast(TargetZipPoint + Vector3.up * 0.05f + Vector3.forward * 0.05f, Vector3.down, out RaycastHit hit, 100f))
                {
                    if (hit.collider.gameObject == ObjectToZip)
                    {
                        Debug.Log("EndZipPoint 1: " + hit.point);
                        return hit.point;
                    }
                }
                Debug.DrawRay(TargetZipPoint + Vector3.up * 0.05f + Vector3.back * 1, Vector3.down * 100, Color.red, 5f);
                if (Physics.Raycast(TargetZipPoint + Vector3.up * 0.05f + Vector3.back * 1, Vector3.down, out hit, 100f))
                {
                    if (hit.collider.gameObject == ObjectToZip)
                    {
                        Debug.Log("EndZipPoint 2: " + hit.point);
                        return hit.point;
                    }
                }
                Debug.DrawRay(TargetZipPoint + Vector3.up * 0.05f + Vector3.left * 0.05f, Vector3.down * 100, Color.red, 5f);
                if (Physics.Raycast(TargetZipPoint + Vector3.up * 0.05f + Vector3.left * 0.05f, Vector3.down, out hit, 100f))
                {
                    if (hit.collider.gameObject == ObjectToZip)
                    {
                        Debug.Log("EndZipPoint 3: " + hit.point);
                        return hit.point;
                    }
                }
                Debug.DrawRay(TargetZipPoint + Vector3.up * 0.05f + Vector3.right * 0.05f, Vector3.down * 100, Color.red, 5f);
                if (Physics.Raycast(TargetZipPoint + Vector3.up * 0.05f + Vector3.right * 0.05f, Vector3.down, out hit, 100f))
                {
                    if (hit.collider.gameObject == ObjectToZip)
                    {
                        Debug.Log("EndZipPoint 4: " + hit.point);
                        return hit.point;
                    }
                }
                return TargetZipPoint;
            }
        }

        public Tween ZipToPoint(Transform target, float timeToZip)
        {
            var targetPosition = new Vector3(TargetZipPoint.x, target.transform.position.y, TargetZipPoint.z);
            var endZipPoint = new Vector3(EndZipPoint.x, target.transform.position.y, EndZipPoint.z);

            if (target.transform.position.y < targetPosition.y)
            {
                Debug.Log("Target is below the zip point");
                //return target.transform.DOMove(targetPosition, timeToZip).OnComplete(() => target.transform.DOMove(endZipPoint, 0.05f));
                return target.transform.DOMove(targetPosition, timeToZip);
            }
            else
            {
                Debug.Log("Target is above the zip point");
                return target.transform.DOMove(targetPosition, timeToZip);
            }
        }
        public void SetActive(bool active, GameObject objectToZip = null)
        {
            ObjectToZip = objectToZip;
            _displayZipPoint.gameObject.SetActive(active);
        }

        public void SetPositions(Vector3 worldPosition)
        {
            _displayZipPoint.position = Camera.main.WorldToScreenPoint(worldPosition);
            TargetZipPoint = worldPosition;
        }
    }
}