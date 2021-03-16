using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    
    public class Pooling : MonoBehaviour
    {
        public static Pooling instance;
        public GameObject poolObject;
        public List<GameObject> pooledObjects;
        public int countObjects;

        private void Awake()
        {
            instance = this;
            pooledObjects = new List<GameObject>();
            for (int i = 0; i < countObjects; i++)
            {
                GameObject obj = Instantiate(poolObject, transform, true);
                obj.SetActive(false);
                pooledObjects.Add(obj);
            }
        }

        private void Start()
        {

        }

        public GameObject GetPooledObject()
        {
            for (int i = 0; i < countObjects; i++)
            {
                if (!pooledObjects[i].activeInHierarchy)
                {
                    pooledObjects[i].SetActive(true);
                    return pooledObjects[i];
                }
            }
            return null;
        }

        private void Update()
        {
        }
    }
    
}