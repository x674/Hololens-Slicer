using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
    public class RandomDropper : MonoBehaviour
    {
        private List<Transform> dropPositionsList;

        private void Awake()
        {
            dropPositionsList = GetComponentsInChildren<Transform>().ToList();
        }

        private void Start()
        {
            StartCoroutine(Drop());
        }

        private void Update()
        {
        }
        private IEnumerator Drop()
        {
            GameObject obj = Pooling.instance.GetPooledObject();
            if (obj != null)
            {
                obj.transform.position = dropPositionsList[Random.Range(0,dropPositionsList.Count)].position; //transform.position;
            }
            WaitForSeconds DropDuration = new WaitForSeconds(Random.Range(.15f,.45f));
            yield return DropDuration;
            StartCoroutine(Drop());

        }
    }
}