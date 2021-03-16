using System;
using EzySlice;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
    public class CollisionRaycast : MonoBehaviour
    {
        public Transform StartPose, EndPose;
        public Material crossSectionMaterial;
        [Range(0.001f,0.020f)]
        public float RadiusCapsule = 0.015f;
        private void Awake()
        {
        }

        private void Start()
        {
            Physics.gravity = Vector3.back;
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawRay(StartPose.position,(EndPose.position-StartPose.position).normalized);

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
            //if (Physics.Raycast(ray,out RaycastHit raycastHit,Vector3.Magnitude(EndPose.position-StartPose.position)))
            if (Physics.CapsuleCast(StartPose.position,EndPose.position,RadiusCapsule,(EndPose.position-StartPose.position).normalized,out RaycastHit raycastHit))
            {
                if (raycastHit.transform.name.Contains("Cube"))
                {
                    //Random.insideUnitSphere
                    EzySlice.Plane plane = new EzySlice.Plane(raycastHit.point, raycastHit.normal);
                    EzySlice.Plane plane2 = new EzySlice.Plane(Random.insideUnitSphere, Random.insideUnitSphere.normalized);
                    GameObject[] shatters =
                        raycastHit.transform.gameObject.SliceInstantiate(plane,
                            new TextureRegion(0.0f, 0.0f, 1.0f, 1.0f),
                            crossSectionMaterial);
                    if (shatters == null)
                    {
                        shatters =
                            raycastHit.transform.gameObject.SliceInstantiate(plane2,
                                new TextureRegion(0.0f, 0.0f, 1.0f, 1.0f),
                                crossSectionMaterial);
                    }
                    
                    //GameObject[] shatters = raycastHit.transform.gameObject.SliceInstantiate(plane, new TextureRegion(0.0f, 0.0f, 1.0f, 1.0f),
                    //    crossSectionMaterial);
                    if (shatters != null && shatters.Length > 0)
                    {
                        Debug.Log("Hit "+ raycastHit.collider + shatters.Length+ " shatters");
                        raycastHit.transform.gameObject.SetActive(false);

                        // add rigidbodies and colliders
                        foreach (GameObject shatteredObject in shatters)
                        {
                            shatteredObject.AddComponent<MeshCollider>().convex = true;
                            Rigidbody rigidbody = shatteredObject.AddComponent<Rigidbody>();
                            rigidbody.mass = 0.05f;
                            
                            //rigidbody.useGravity = false;
                            //rigidbody.mass = 0.01f;

                            //prevShatters.Add(shatteredObject);
                        }
                    }
                }

                
            }
            
        }
    }
}