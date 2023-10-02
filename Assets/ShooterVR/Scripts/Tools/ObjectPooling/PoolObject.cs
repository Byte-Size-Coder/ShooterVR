using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolObject : MonoBehaviour
{
    [SerializeField] private int poolId;

    public void SetPoolId(int id)
    {
        poolId = id;
    }

    public int getPoolId()
    {
        return poolId;
    }
}
