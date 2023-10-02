using UnityEngine;

namespace BSC.SVR.Game
{
    public class StartGameTarget : MonoBehaviour
    {
        [SerializeField] private GameObject target;
        [SerializeField] private BoxCollider collider;
        [SerializeField] private ParticleSystem deathEffect;

        public void OnHit()
        {
            GameObject effect = Instantiate(deathEffect.gameObject, transform.position, transform.rotation);
            Destroy(effect, 2.0f);

            target.SetActive(false);
            collider.enabled = false;
            StatTracking.Instance.StartGame();
        }

        public void SpawnTarget()
        {
            target.SetActive(true);
            collider.enabled = true;
        }
    }
}

