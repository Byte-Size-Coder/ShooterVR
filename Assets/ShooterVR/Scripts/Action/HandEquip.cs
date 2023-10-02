using BSC.SVR.Combat;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

namespace BSC.SVR.Action
{
    public class HandEquip : MonoBehaviour
    {
        public InputActionReference primaryButtonReference;
        public string orientation;

        private Gun currentEquippedGun = null;

        private void Start()
        {
            primaryButtonReference.action.performed += ToggleDrum;
        }

        private void OnDestroy()
        {
            primaryButtonReference.action.performed -= ToggleDrum;
        }

        public void Equip(SelectEnterEventArgs args)
        {
            Gun gun = args.interactableObject.transform.GetComponent<Gun>();

            if (gun == null) return;

            currentEquippedGun = gun;
        }

        public void UnEquip()
        {
            if (currentEquippedGun == null) return;
            currentEquippedGun = null;
        }

        private void Reload()
        {
            if (currentEquippedGun == null || currentEquippedGun.IsAmmoFull()) return;

            currentEquippedGun.Reload();
        }

        private void ToggleDrum(InputAction.CallbackContext context)
        {
            if (currentEquippedGun == null) return;

            currentEquippedGun.ToggleDrum(orientation);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Reload")
            {
                Holster holster = other.GetComponent<Holster>();
                if (holster)
                {
                    Reload();
                }
            }
        }
    }
}
