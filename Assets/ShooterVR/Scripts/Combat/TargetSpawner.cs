using UnityEngine;

namespace BSC.SVR.Combat
{
    public class TargetSpawner : MonoBehaviour
    {
        [Header("Spawn Info")]
        [SerializeField] private Vector3 spawnSize;
        [SerializeField] private float spawnSpeed;
        [SerializeField] private PoolObject[] targetPrefabs;

        private float spawnTimer = 0.0f;
        private bool startSpawner = false;

        public void SetStartSpawner(bool value)
        {
            startSpawner = value;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(0, 1, 0, 0.5f);
            Gizmos.DrawCube(transform.position, spawnSize);
        }


        void Update()
        {
            if (!startSpawner) return;

            spawnTimer += Time.deltaTime;

            if (spawnTimer > spawnSpeed)
            {
                SpawnTarget();
                spawnTimer = 0.0f;
            }
        }

        private void SpawnTarget()
        {
            int randomTargetIndex = Random.Range(0, targetPrefabs.Length);

            PoolObject targetPrefab = targetPrefabs[randomTargetIndex];

            Vector3 randomPos = transform.position + new Vector3(
                Random.Range(-spawnSize.x / 2, spawnSize.x / 2),
                Random.Range(-spawnSize.y / 2, spawnSize.y / 2), 
                Random.Range(-spawnSize.y / 2, spawnSize.y / 2)
            );

            GameObject target = Pool.SpawnObject(targetPrefab);
            target.transform.position = randomPos;
        }
    }
}

