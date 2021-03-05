using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class everyZero : MonoBehaviour
    {
        private void LateUpdate()
        {
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.Euler(Vector3.zero);
        }
    }
}