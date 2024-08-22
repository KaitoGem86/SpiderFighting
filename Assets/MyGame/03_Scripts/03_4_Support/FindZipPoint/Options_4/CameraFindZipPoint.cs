using System;
using Animancer;
using UnityEngine;

namespace Core.GamePlay.Support
{
    public class CameraFindZipPoint : MonoBehaviour
    {
        public new Camera camera;
        public float distance = 40f;
        public float projectDistance = 40f;
        public LayerMask layerMask;
        public LayerMask GroundLayer;
        public RaycastHit hitSurface;
        public float inwardsOffset = 0.1f;

        public Vector3 zipPoint;
        public Vector3 pointSetBack;
        public Vector3 upPoint;

        public Vector3 forwardPoint;
        public float forwardPointDistance = 10f;
        public float backPOffset = 0.2f;
        public float floorDistance = 40f;

        public float cameraSphereRadius = 0.5f;
        public float upPointSphereRadius = 0.5f;
        public float upSphereCastHeight = 50f;

        //public GameObject focusZipPointPrefab;
        public RectTransform focusPrefabRectTransform;

        private void Update()
        {
            if (Physics.SphereCast(camera.transform.position, cameraSphereRadius, camera.transform.forward,
                    out RaycastHit hit, distance, layerMask))
            {
                hitSurface = hit;
                pointSetBack = hit.point - hit.normal * inwardsOffset;
                if (Vector3.Dot(hit.normal, Vector3.up) <= 0.99f)
                {
                    if (Physics.Raycast(camera.transform.position, camera.transform.forward, distance, GroundLayer))
                    {
                        zipPoint = Vector3.zero;
                        //Debug.Log("Found ground");
                        if (focusPrefabRectTransform.gameObject.activeSelf)
                        {
                            focusPrefabRectTransform.gameObject.SetActive(false);
                        }
                    }
                    else
                    {
                        //Debug.Log("FaceZipPoint");
                        FaceZipPoint();
                    }
                }
                else
                {
                    //Debug.Log("FaceZipDownPoint");
                    FaceDownZipPoint();
                }
            }
            else
            {
                zipPoint = Vector3.zero;
                if (focusPrefabRectTransform.gameObject.activeSelf)
                {
                    focusPrefabRectTransform.gameObject.SetActive(false);
                }
            }
        }
        
        private bool IsZipPointInView(Vector3 point)
        {
            Vector3 screenPoint = camera.WorldToViewportPoint(point);
            return screenPoint.z > 0 && screenPoint.x >= 0 && screenPoint.x <= 1 && screenPoint.y >= 0 && screenPoint.y <= 1;
        }

        private void FaceZipPoint()
        {
            var direc = Vector3.ProjectOnPlane(Vector3.up, hitSurface.normal);
            if (Physics.Raycast(pointSetBack, direc, out RaycastHit hit2, projectDistance, layerMask))
            {
                upPoint = hit2.point + Vector3.up * upSphereCastHeight;
                //Debug.DrawRay(upPoint, -hit2.transform.up, Color.blue);
                if (Physics.SphereCast(upPoint, upPointSphereRadius, -hit2.transform.up, out RaycastHit hit3,
                        upSphereCastHeight,
                        layerMask))
                {
                    //Debug.DrawRay(hit3.point, hit3.normal, Color.magenta);
                    if (Vector3.Angle(hit3.normal, Vector3.up) < 45)
                    {
                        zipPoint = hit3.point;
                        ShowFocusZipPoint();
                    }
                    else
                    {
                        zipPoint = Vector3.zero;
                        if (focusPrefabRectTransform.gameObject.activeSelf)
                        {
                            focusPrefabRectTransform.gameObject.SetActive(false);
                        }
                    }
                }
                else
                {
                    zipPoint = Vector3.zero;
                    if (focusPrefabRectTransform.gameObject.activeSelf)
                    {
                        focusPrefabRectTransform.gameObject.SetActive(false);
                    }
                }
            }
            else
            {
                zipPoint = Vector3.zero;
                if (focusPrefabRectTransform.gameObject.activeSelf)
                {
                    focusPrefabRectTransform.gameObject.SetActive(false);
                }
            }
        }

        private void FaceDownZipPoint()
        {
            var direc = Vector3.ProjectOnPlane(camera.transform.forward, hitSurface.normal);
            forwardPoint = pointSetBack + direc.normalized * forwardPointDistance;
            //var distanceFD = Vector3.Distance(direc, pointSetBack);
            //Debug.DrawLine(pointSetBack, forwardPoint, Color.red);

            #region Tutruocvao

            if (Physics.Raycast(forwardPoint, -direc, out RaycastHit hit2, floorDistance, layerMask))
            {
                //Debug.Log("hit2 - FaceDownZipPoint");
                var hit2Point = hit2.point + -hit2.normal * backPOffset;
                if (Physics.Raycast(hit2Point, Vector3.up, out RaycastHit hit3, distance, layerMask))
                {
                    //Debug.DrawLine(hit2Point, hit2Point + Vector3.up, Color.yellow);
                    //Debug.Log("hit 3 - FaceDownZipPoint");
                    
                    var up = hit3.point + Vector3.up * 0.2f;
                    if (Physics.Raycast(up, -direc, out RaycastHit hit4, 5, layerMask))
                    {
                        var back = hit4.point + -hit4.normal * backPOffset;
                        if (Physics.Raycast(back, Vector3.up, out RaycastHit hit5, 10, layerMask))
                        {
                            if (Vector3.Angle(hit5.normal, Vector3.up) < 45)
                            {
                                zipPoint = hit5.point;
                                ShowFocusZipPoint();
                            }
                            else
                            {
                                zipPoint = Vector3.zero;
                                if (focusPrefabRectTransform.gameObject.activeSelf)
                                {
                                    focusPrefabRectTransform.gameObject.SetActive(false);
                                }
                            }
                        }
                        else
                        {
                            zipPoint = hit3.point;
                            ShowFocusZipPoint();
                        }
                    }
                    else if (Physics.Raycast(up, direc, out RaycastHit hit6, 5, layerMask))
                    {
                        //Debug.Log("hit6 - FaceDownZipPoint");
                        var forward = hit6.point + -hit6.normal * backPOffset;
                        if (Physics.Raycast(forward, Vector3.up, out RaycastHit hit7, 10, layerMask))
                        {
                            if (Vector3.Angle(hit7.normal, Vector3.up) < 45)
                            {
                                zipPoint = hit7.point;
                                ShowFocusZipPoint();
                            }
                            else
                            {
                                zipPoint = Vector3.zero;
                                if (focusPrefabRectTransform.gameObject.activeSelf)
                                {
                                    focusPrefabRectTransform.gameObject.SetActive(false);
                                }
                            }
                        }
                        else
                        {
                            zipPoint = hit3.point;
                            ShowFocusZipPoint();
                        }
                    }
                    else
                    {
                        zipPoint = hit3.point;
                        ShowFocusZipPoint();
                    }
                }
                else
                {
                    //Debug.Log("hit3 - FaceDownZipPoint - Null");
                    zipPoint = Vector3.zero;
                    if (focusPrefabRectTransform.gameObject.activeSelf)
                    {
                        focusPrefabRectTransform.gameObject.SetActive(false);
                    }
                }
            }
            else
            {
                //Debug.Log("hit2 - FaceDownZipPoint null");
                zipPoint = Vector3.zero;
                if (focusPrefabRectTransform.gameObject.activeSelf)
                {
                    focusPrefabRectTransform.gameObject.SetActive(false);
                }
            }

            #endregion
        }

        private void ShowFocusZipPoint()
        {
            Vector3 screenPoint = camera.WorldToScreenPoint(zipPoint);
            if (focusPrefabRectTransform != null)
            {
                focusPrefabRectTransform.position = screenPoint;
                if (focusPrefabRectTransform.gameObject.activeSelf == false)
                {
                    focusPrefabRectTransform.gameObject.SetActive(true);
                }
            }
        }


        // private void OnDrawGizmos()
        // {
        //     Gizmos.color = Color.green;
        //     Gizmos.DrawSphere(pointSetBack, 0.5f);
        //     Gizmos.color = Color.yellow;
        //     Gizmos.DrawRay(upPoint, Vector3.down * upSphereCastHeight);
        //     Gizmos.color = Color.blue;
        //     Gizmos.DrawSphere(zipPoint, 0.5f);
        // }
    }
}




