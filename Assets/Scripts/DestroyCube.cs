using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class DestroyCube : MonoBehaviour
    {
        private Rigidbody rigidbody;
        private bool sliced = false;
        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            if (transform.position.z <-40)
            {
                rigidbody.velocity = Vector3.zero;
                this.gameObject.SetActive(false);
                if (sliced)
                {
                    Destroy(gameObject);
                }
            }
        }

        public void SetSliced()
        {
            sliced = true;
        }
    }
}