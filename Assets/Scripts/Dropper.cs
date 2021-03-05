using System;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace DefaultNamespace
{
    public class Dropper : MonoBehaviour
    {
        private WaitForSeconds DropDuration = new WaitForSeconds(.75f);

        private void Start()
        {
            StartCoroutine(Drop());
        }

        private void Update()
        {
            foreach (var o in Pooling.instance.pooledObjects.Where(e => e.activeInHierarchy))
            {
                if (o.transform.position.y<=-30)
                {
                    Rigidbody rigidbody = o.GetComponent<Rigidbody>();
                    o.SetActive(false);
                    o.transform.rotation = Quaternion.Euler(Vector3.zero);
                    rigidbody.angularVelocity = Vector3.zero;
                    rigidbody.velocity = Vector3.zero;
                }
            }
        }

        private IEnumerator Drop()
        {
            yield return DropDuration;
            Debug.Log("drop!");
            GameObject obj = Pooling.instance.GetPooledObject();
            obj.transform.position = Vector3.zero;
            //-45
            StartCoroutine(Drop());

        }

    }
}