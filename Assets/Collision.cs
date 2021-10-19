using System;
using System.Collections;
using System.Collections.Generic;
using EzySlice;
using UnityEngine;

public class Collision : MonoBehaviour
{
    public Material crossSectionMaterial;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnCollisionStay(UnityEngine.Collision other)
    {
        if (!other.gameObject.name.Contains("Lower_Hull") && !other.gameObject.name.Contains("Upper_Hull"))
        {
            //Debug.Log("Collision Enter on " + other.transform.name);
            // Debug.DrawRay(other.contacts[0].point,other.contacts[0].normal);
            EzySlice.Plane plane = new EzySlice.Plane(other.contacts[0].point, other.contacts[0].normal);
            GameObject[] shatters = other.gameObject.SliceInstantiate(plane, new TextureRegion(0.0f, 0.0f, 1.0f, 1.0f),
                crossSectionMaterial);
            if (shatters != null && shatters.Length > 0)
            {
                other.gameObject.SetActive(false);

                // add rigidbodies and colliders
                foreach (GameObject shatteredObject in shatters)
                {
                    shatteredObject.AddComponent<MeshCollider>().convex = true;
                    Rigidbody rigidbody = shatteredObject.AddComponent<Rigidbody>();
                    //rigidbody.useGravity = false;
                    rigidbody.mass = 0.01f;

                    //prevShatters.Add(shatteredObject);
                }
            }
        }
        // Debug.Log("Collision Enter on " + other.transform.name);
        // Debug.DrawRay(other.contacts[0].point,other.contacts[0].normal);
        //EzySlice.Plane plane = new EzySlice.Plane(other.contacts[0].point,other.contacts[0].normal);
        //other.gameObject.SliceInstantiate(plane, new TextureRegion(0.0f, 0.0f, 1.0f, 1.0f),crossSectionMaterial);
        // other.gameObject.Slice(plane, Material);
        //Debug.DrawRay( other. contactCube.point,contactCube.normal);
        // foreach (var contactCube in other.contacts)
        // {
        //     GameObject gameObject = contactCube.thisCollider.gameObject;
        //     Debug.DrawRay(contactCube.point,contactCube.normal);
        //     EzySlice.Plane plane = new EzySlice.Plane(contactCube.point,contactCube.normal);
        //     gameObject.Slice(plane, Material);
        //     //EzySlice.Plane plane = new EzySlice.Plane(contactCube.normal, contactCube.point);
        //     //SlicedHull.Slice(gameObject, contactCube.normal, contactCube.point, Material);
        //     //Slicer.Slice(gameObject, plane, new TextureRegion(0.0f, 0.0f, 1.0f, 1.0f), Material);
        //     //contactCube.thisCollider.gameObject.Slice(contactCube.thisCollider.gameObject,contactCube.normal,contactCube.point, Material);
        // }
        // Debug.Log("Collision Stay on " + other.transform.name);
    }

    private void OnCollisionEnter(UnityEngine.Collision other)
    {
        Debug.Log("");
        // if (!other.gameObject.name.Contains("Lower_Hull") && !other.gameObject.name.Contains("Upper_Hull"))
        // {
        //     //Debug.Log("Collision Enter on " + other.transform.name);
        //     // Debug.DrawRay(other.contacts[0].point,other.contacts[0].normal);
        //     EzySlice.Plane plane = new EzySlice.Plane(other.contacts[0].point, other.contacts[0].normal);
        //     GameObject[] shatters = other.gameObject.SliceInstantiate(plane, new TextureRegion(0.0f, 0.0f, 1.0f, 1.0f),
        //         crossSectionMaterial);
        //     if (shatters != null && shatters.Length > 0)
        //     {
        //         other.gameObject.SetActive(false);
        //
        //         // add rigidbodies and colliders
        //         foreach (GameObject shatteredObject in shatters)
        //         {
        //             shatteredObject.AddComponent<MeshCollider>().convex = true;
        //             Rigidbody rigidbody = shatteredObject.AddComponent<Rigidbody>();
        //             //rigidbody.useGravity = false;
        //             rigidbody.mass = 0.01f;
        //
        //             //prevShatters.Add(shatteredObject);
        //         }
        //     }
        // }


        //other.gameObject.Slice(plane, Material);
        // foreach (var contactCube in other.contacts)
        // {
        //     GameObject gameObject = contactCube.thisCollider.gameObject;
        //     Debug.DrawRay(contactCube.point,contactCube.normal);
        //     EzySlice.Plane plane = new EzySlice.Plane(contactCube.point,contactCube.normal);
        //     gameObject.Slice(plane, Material);
        //     //EzySlice.Plane plane = new EzySlice.Plane(contactCube.normal, contactCube.point);
        //     //SlicedHull.Slice(gameObject, contactCube.normal, contactCube.point, Material);
        //     //Slicer.Slice(gameObject, plane, new TextureRegion(0.0f, 0.0f, 1.0f, 1.0f), Material);
        //     //contactCube.thisCollider.gameObject.Slice(contactCube.thisCollider.gameObject,contactCube.normal,contactCube.point, Material);
        // }
    }
}