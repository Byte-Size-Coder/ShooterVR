using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private List<PoolObject> pool;
    private PoolObject prefab;


    public void InitializePool(PoolObject obj, int size)
    {
        // Intialize List and store reference to this pool's object
        pool = new List<PoolObject>();
        prefab = obj;

        // Create Objects to store in Pool
        for (int i = 0; i < size; ++i)
        {
            PoolObject thisObj = Instantiate(prefab, transform.position, Quaternion.identity);
            thisObj.transform.SetParent(gameObject.transform);
            thisObj.gameObject.SetActive(false);
            pool.Add(thisObj);
        }
    }

    public GameObject GetObject()
    {
        // Check if there is any objects in the pool
        if (pool.Count > 0)
        {
            // If there are objects in the pool, grab the first object and remove from pool
            PoolObject obj = pool[0];
            pool.RemoveAt(0);

            return obj.gameObject;
        }
        else
        {
            // If there are no objects in pool, create a new object to use
            // NOTE: This should not be used much, make sure to have a good reasonable initial size of pool to limit creating objects
            PoolObject obj = Instantiate(prefab, transform.position, Quaternion.identity);
            obj.transform.SetParent(gameObject.transform);

            return obj.gameObject;
        }

    }

    public void ReturnObject(PoolObject obj)
    {
        // Retun object back to the pool and reset its properties
        pool.Add(obj);
        obj.transform.SetParent(gameObject.transform);
        obj.transform.position = transform.position;
        obj.gameObject.SetActive(false);
    }
}