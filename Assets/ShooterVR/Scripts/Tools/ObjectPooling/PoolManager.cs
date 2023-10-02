using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BSC.Tools.ObjectPooling
{
    public class PoolManager : MonoBehaviour
    {
        public GameObject poolPrefab;
        public PoolData[] pools;

        private Dictionary<int, ObjectPool> poolList;

        // Start is called before the first frame update
        void Start()
        {
            InitilalizeAllPools();
            Pool.SetPoolManagerReference(this);
        }

        public void InitilalizeAllPools()
        {
            //Initialize the pool list
            poolList = new Dictionary<int, ObjectPool>();

            // Create all pool Gameobjects and initialize them with the correct objects and sizes;
            for (int i = 0; i < pools.Length; ++i)
            {
                GameObject obj = Instantiate(poolPrefab);

                obj.transform.position = gameObject.transform.position;
                obj.transform.SetParent(gameObject.transform);

                ObjectPool pool = obj.GetComponent<ObjectPool>();
               // obj.name = " " + pools[i].name;
                pool.InitializePool(pools[i].prefab, pools[i].size);

                poolList.Add(pools[i].prefab.getPoolId(), pool);
            }

        }

        public GameObject FindObject(PoolObject prefab)
        {
            // Finds the correct pool and grabs the object
            return poolList[prefab.getPoolId()].GetObject();
        }

        public void PutBackObject(GameObject obj)
        {
            PoolObject poolObj = GetPoolObject(obj);


            // Returns the object to the correct pool
            poolList[poolObj.getPoolId()].ReturnObject(poolObj);
        }

        private PoolObject GetPoolObject(GameObject obj)
        {
            PoolObject poolObj = obj.GetComponent<PoolObject>();

            if (poolObj == null)
            {
                throw new System.Exception($"Error: Pooling object {obj.name} must have PoolObject Script to access pools");
            }

            return poolObj;
        }
    }
}

