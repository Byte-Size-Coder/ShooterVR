using BSC.Tools.ObjectPooling;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * This is a static class to handle the spawning and despawning of objects from the object pools
 * Athuor: Matthew Douglas
 **/

[System.Serializable]
public struct PoolData
{
    public PoolObject prefab;
    public int size;
}


public static class Pool
{
    static PoolManager poolManager;

    public static void SetPoolManagerReference(PoolManager manager)
    {
        poolManager = manager;
    }

    public static GameObject SpawnObject(PoolObject prefab)
    {
        GameObject obj = poolManager.FindObject(prefab);
        obj.SetActive(true);
        return obj;

    }

    public static void RemoveObject(GameObject obj)
    {
        poolManager.PutBackObject(obj);
    }
}