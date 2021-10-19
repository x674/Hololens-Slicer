using System;
using Assets.Scripts.SlicerGame;
using EzySlice;
using UnityEngine;

namespace DefaultNamespace
{
    public class CollisionRaycast : MonoBehaviour
    {
        public Transform StartPose, EndPose;
        public Material crossSectionMaterial;
        [Range(0.001f,0.020f)]
        public float RadiusCapsule = 0.015f;

        private float raycastDistance;

        private void Start()
        {
            Physics.gravity = Vector3.back;
        }

        private void Update()
        {
            //Debug.Log("magnitude "+EndPose.position.magnitude);
            //Debug.Log("x "+EndPose.position.x+" "+"y "+EndPose.position.y+" "+"z "+EndPose.position.z+
            Vector3 endPositionLine = (((StartPose.position-EndPose.position).normalized)* -1)+StartPose.position;
            //Debug.Log("endPositionLine "+endPositionLine);
            //Debug.DrawLine(StartPose.position,endPositionLine ,Color.magenta);
            Ray ray = new Ray
            {
                origin = StartPose.position, direction = (EndPose.position -StartPose.position).normalized
            };
            int layerMask = 1 << 8;
            string s = Convert.ToString(layerMask, toBase: 2);
            int layerMask1 = ~(1 << 8);
            string s2 = Convert.ToString(layerMask1, toBase: 2);
            //if (Physics.Raycast(ray,out RaycastHit raycastHit,Vector3.Magnitude(EndPose.position-StartPose.position)))
            if (Physics.CapsuleCast(StartPose.position,EndPose.position,RadiusCapsule,transform.forward,out RaycastHit raycastHit,Mathf.Infinity,layerMask))
            {
                //if (raycastHit.transform.name.Contains("Cube"))
                //{
                    // //Random.insideUnitSphere
                    // EzySlice.Plane plane = new EzySlice.Plane(raycastHit.point, raycastHit.normal);
                    // EzySlice.Plane plane2 = new EzySlice.Plane(Random.insideUnitSphere, Random.insideUnitSphere.normalized);
                    // GameObject[] shatters =
                    //     raycastHit.transform.gameObject.SliceInstantiate(plane,
                    //         new TextureRegion(0.0f, 0.0f, 1.0f, 1.0f),
                    //         crossSectionMaterial);
                    // if (shatters == null)
                    // {
                    //     shatters =
                    //         raycastHit.transform.gameObject.SliceInstantiate(plane2,
                    //             new TextureRegion(0.0f, 0.0f, 1.0f, 1.0f),
                    //             crossSectionMaterial);
                    // }
                    //
                    // //GameObject[] shatters = raycastHit.transform.gameObject.SliceInstantiate(plane, new TextureRegion(0.0f, 0.0f, 1.0f, 1.0f),
                    // //    crossSectionMaterial);
                    // if (shatters != null && shatters.Length > 0)
                    // {
                    //     Debug.Log("Hit "+ raycastHit.collider + shatters.Length+ " shatters");
                    //     raycastHit.transform.gameObject.SetActive(false);
                    //
                    //     // add rigidbodies and colliders
                    //     foreach (GameObject shatteredObject in shatters)
                    //     {
                    //         shatteredObject.AddComponent<MeshCollider>().convex = true;
                    //         Rigidbody rigidbody = shatteredObject.AddComponent<Rigidbody>();
                    //         rigidbody.mass = 0.05f;
                    //         
                    //         //rigidbody.useGravity = false;
                    //         //rigidbody.mass = 0.01f;
                    //
                    //         //prevShatters.Add(shatteredObject);
                    //     }
                    // }
                    Slicable slicable = raycastHit.transform.GetComponent<Slicable>();
                    Debug.DrawRay(raycastHit.point, raycastHit.normal);
                    if (slicable != null)
                    {
                        GameObject[] sliced = slicable.GetCutWithoutDestroy(new Ray(raycastHit.point, raycastHit.normal));
                        if (sliced != null)
                        {
                            for (int i = 0; i < sliced.Length; i++)
                            {
                                sliced[i].layer = 0;
                                sliced[i].GetComponent<DestroyCube>()?.SetSliced();
                            }
                        }
                }
            //}
            }
            Debug.DrawRay(raycastHit.point,raycastHit.normal);
        }
        private void OnDrawGizmos()
        {
            Gizmos.DrawSphere(EndPose.position,RadiusCapsule);
            Gizmos.DrawSphere(StartPose.position,RadiusCapsule);
            Gizmos.DrawLine(StartPose.position,EndPose.position); 
            Gizmos.DrawWireCube((StartPose.position+EndPose.position)/2,new Vector3(RadiusCapsule,(EndPose.position-StartPose.position).magnitude,RadiusCapsule));
        }
    }
}