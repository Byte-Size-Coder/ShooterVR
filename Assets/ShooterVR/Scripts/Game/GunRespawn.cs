
using UnityEngine;

public class GunRespawn : MonoBehaviour
{
    [SerializeField] private Transform gunSpawnTransform;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pistol"))
        {
            other.transform.position = gunSpawnTransform.transform.position;
        }
    }
}
