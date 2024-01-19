using UnityEngine;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;

namespace BSC.SVR.Combat
{
    public class Holster : XRSocketInteractor
    {
        [SerializeField] private TMP_Text reloadText;
        [SerializeField] private string targetTag;

        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip holsterClip;
        [SerializeField] private AudioClip drawClip;
        
        // Start is called before the first frame update
        public override bool CanHover(IXRHoverInteractable interactable)
        {
            var gun = interactable.transform.GetComponent<Gun>();
            if (gun == null)
            {
                return base.CanHover(interactable);
            }
            
            return base.CanHover(interactable) && !gun.CheckDrumOpen();
            
        }

        public override bool CanSelect(IXRSelectInteractable interactable)
        {
            var gun = interactable.transform.GetComponent<Gun>();
            if (gun == null)
            {
                return base.CanSelect(interactable);
            }
            
            return base.CanSelect(interactable) && !gun.CheckDrumOpen();
        }

        public void HolsterWeapon()
        {
            audioSource.PlayOneShot(holsterClip);
        }

        public void DrawWeapon()
        {
            audioSource.PlayOneShot(drawClip);
        }
        
        public void ShowReload()
        {
            reloadText.gameObject.SetActive(true);
        }

        public void HideReload()
        {
            reloadText.gameObject.SetActive(false);
        }

        private bool MatchUsingTag(XRBaseInteractable interactable)
        {
            return interactable.CompareTag(targetTag);
        }
    }
}

