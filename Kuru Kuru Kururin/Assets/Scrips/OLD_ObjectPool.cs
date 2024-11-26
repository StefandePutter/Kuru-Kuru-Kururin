using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool SharedInstance;
    public List<GameObject> pooledObjects;
    public GameObject objectToPool;
    public int amountToPool;

    private void Awake()
    {
        SharedInstance = this;
    }

    private void Start()
    {
        pooledObjects = new List<GameObject>();
        GameObject tmp;

        GameObject parent = new GameObject();
        parent.name = "Projectile Pool";

        for (int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(objectToPool);
            tmp.SetActive(false);
            tmp.transform.parent = parent.transform;
            pooledObjects.Add(tmp);
        }
    }

    public GameObject GetPooledObject()
    {
        // give back an unactive GameObject projectile
        for(int i = 0;i < amountToPool;i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        // return null if there are none left
        return null;
    }
}
