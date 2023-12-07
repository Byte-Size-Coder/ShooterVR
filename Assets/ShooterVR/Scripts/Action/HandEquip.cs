using BSC.SVR.Combat;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

namespace BSC.SVR.Action
{
    public class HandEquip : MonoBehaviour
    {
        public InputActionProperty angularVelocityProperty;
        public string orientation;
        
        private Gun _currentEquippedGun;
        private FlickDetector _flickDetector;
        
        private void Awake()
        {
            _flickDetector = GetComponent<FlickDetector>();
        }

        private void Update()
        {
            if (_currentEquippedGun == null || _flickDetector == null) return;
            _flickDetector.CheckFlick(this);
        }

        public void Equip(SelectEnterEventArgs args)
        {
            Gun gun = args.interactableObject.transform.GetComponent<Gun>();

            if (gun == null) return;

            _currentEquippedGun = gun;
        }

        public float GetRotationSpeed()
        {
            return angularVelocityProperty.action.ReadValue<Vector3>().magnitude;
        }
        
        public void UnEquip()
        {
            _currentEquippedGun = null;
        }

        public void FlickToggleDrum()
        {
            if (_currentEquippedGun == null) return;

            _currentEquippedGun.ToggleDrum(orientation);
        }

        private void Reload()
        {
            if (_currentEquippedGun == null || _currentEquippedGun.IsAmmoFull()) return;

            _currentEquippedGun.Reload();
        }
        

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Reload")) return;
            var holster = other.GetComponent<Holster>();
            if (holster)
            {
                Reload();
            }
        }
    }
}
