using DG.Tweening;
using UnityEngine;

namespace Core.GamePlay.Support
{
    public class DisplayZipPoint : MonoBehaviour
    {
        private GameObject ObjectToZip { get; set; }
        private Vector3 TargetZipPoint => transform.position + Vector3.up;
        private Vector3 EndZipPoint
        {
            get
            {
                Debug.DrawRay(transform.position + Vector3.up * 0.05f + Vector3.forward * 0.05f, Vector3.down, Color.red, 100f);
                Debug.Log(ObjectToZip.name);
                if (Physics.Raycast(transform.position + Vector3.up * 0.05f + Vector3.forward * 0.05f, Vector3.down, out RaycastHit hit, 100f))
                {
                    if (hit.collider.gameObject == ObjectToZip)
                    {
                        Debug.Log("EndZipPoint 1: " + hit.point);
                        return hit.point;
                    }
                }
                Debug.DrawRay(transform.position + Vector3.up * 0.05f + Vector3.back * 0.05f, Vector3.down, Color.red, 100f);
                if (Physics.Raycast(transform.position + Vector3.up * 0.05f + Vector3.back * 1, Vector3.down, out hit, 100f))
                {
                    if (hit.collider.gameObject == ObjectToZip)
                    {
                        Debug.Log("EndZipPoint 2: " + hit.point);
                        return hit.point;
                    }
                }
                Debug.DrawRay(transform.position + Vector3.up * 0.05f + Vector3.left * 0.05f, Vector3.down, Color.red, 100f);
                if (Physics.Raycast(transform.position + Vector3.up * 0.05f + Vector3.left * 0.05f, Vector3.down, out hit, 100f))
                {
                    if (hit.collider.gameObject == ObjectToZip)
                    {
                        Debug.Log("EndZipPoint 3: " + hit.point);
                        return hit.point;
                    }
                }
                Debug.DrawRay(transform.position + Vector3.up * 0.05f + Vector3.right * 0.05f, Vector3.down, Color.red, 100f);
                if (Physics.Raycast(transform.position + Vector3.up * 0.05f + Vector3.right * 0.05f, Vector3.down, out hit, 100f))
                {
                    if (hit.collider.gameObject == ObjectToZip)
                    {
                        Debug.Log("EndZipPoint 4: " + hit.point);
                        return hit.point;
                    }
                }
                return transform.position;
            }
        }

        public Tween ZipToPoint(Transform target, float timeToZip)
        {
            var targetPosition = new Vector3(TargetZipPoint.x, target.transform.position.y, TargetZipPoint.z);
            var endZipPoint = new Vector3(EndZipPoint.x, target.transform.position.y, EndZipPoint.z);

            if (target.transform.position.y < transform.position.y)
            {
                Debug.Log("Target is below the zip point");
                return target.transform.DOMove(targetPosition, timeToZip).OnComplete(() => ObjectToZip.transform.DOMove(endZipPoint, 0.05f));
            }
            else
            {
                Debug.Log("Target is above the zip point");
                return target.transform.DOMove(endZipPoint, timeToZip);
            }
        }
        public void SetActive(bool active, GameObject objectToZip = null)
        {
            ObjectToZip = objectToZip;
            if (objectToZip != null)
                gameObject.transform.rotation = Quaternion.LookRotation(objectToZip.transform.position - transform.position);
            gameObject.SetActive(active);
        }
    }
}