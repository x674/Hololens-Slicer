using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;
using Random = UnityEngine.Random;

/**
 * Represents a really badly written shatter script! use for reference purposes only.
 */
public class RuntimeShatterExample : MonoBehaviour {

    public GameObject objectToShatter;
    public Material crossSectionMaterial;
    //public GameObject PlaneSlice;
    public List<GameObject> prevShatters = new List<GameObject>();

    private void Start()
    {
        //PlaneSlice = GameObject.CreatePrimitive(PrimitiveType.Plane);
    }

    public GameObject[] ShatterObject(GameObject obj, Material crossSectionMaterial = null) {
        return obj.SliceInstantiate(GetRandomPlane(obj.transform.position, obj.transform.localScale),
                                                            new TextureRegion(0.0f, 0.0f, 1.0f, 1.0f),
                                                            crossSectionMaterial);
    }

    public EzySlice.Plane GetRandomPlane(Vector3 positionOffset, Vector3 scaleOffset) {
        Vector3 randomPosition = Random.insideUnitSphere;

        //randomPosition += positionOffset;

        Vector3 randomDirection = Random.insideUnitSphere.normalized;
        EzySlice.Plane plane = new EzySlice.Plane(randomPosition, randomDirection);
        //plane.Compute(this.transform);
        //plane.OnDebugDraw();
        //PlaneSlice.transform.position = randomPosition;
        //PlaneSlice.transform.rotation = Quaternion.Euler(randomDirection);
        //Matrix4x4 matrix4X4 = Matrix4x4.zero;
        //matrix4X4.rotation;
        return plane; //new EzySlice.Plane(randomPosition, randomDirection);
    }

    public void RandomShatter() {
        if (prevShatters.Count == 0) {
            GameObject[] shatters = ShatterObject(objectToShatter, crossSectionMaterial);

            if (shatters != null && shatters.Length > 0) {
                objectToShatter.SetActive(false);

                // add rigidbodies and colliders
                foreach (GameObject shatteredObject in shatters) {
                    shatteredObject.AddComponent<MeshCollider>().convex = true;
                    shatteredObject.AddComponent<Rigidbody>();

                    prevShatters.Add(shatteredObject);
                }
            }

            return;
        }

        // otherwise, shatter the previous shattered objects, randomly picked
        GameObject randomObject = prevShatters[Random.Range(0, prevShatters.Count - 1)];

        GameObject[] randShatter = ShatterObject(randomObject, crossSectionMaterial);

        if (randShatter != null && randShatter.Length > 0) {
            randomObject.SetActive(false);

            // add rigidbodies and colliders
            foreach (GameObject shatteredObject in randShatter) {
                shatteredObject.AddComponent<MeshCollider>().convex = true;
                shatteredObject.AddComponent<Rigidbody>();

                prevShatters.Add(shatteredObject);
            }
        }
    }
}
