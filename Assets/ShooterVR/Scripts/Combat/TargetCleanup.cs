using UnityEngine;

namespace BSC.SVR.Combat
{
    public class TargetCleanup : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {

            if (other.tag == "Target")
            {
                //Destroy(other.gameObject);
                Pool.RemoveObject(other.gameObject);
            }
        }
    }
}

