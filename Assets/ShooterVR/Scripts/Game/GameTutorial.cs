using BSC.SVR.Action;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GameTutorial : MonoBehaviour
{
    [SerializeField] private XRSocketInteractor holsterRight;
    [SerializeField] private XRSocketInteractor holsterLeft;

    [SerializeField] private XRGrabInteractable gun1;
    [SerializeField] private XRGrabInteractable gun2;

    [SerializeField] private FlickDetector flickDetectorRight;
    [SerializeField] private FlickDetector flickDetectorLeft;

    [SerializeField] private HandEquip handEquipRight;
    [SerializeField] private HandEquip handEquipLeft;

    [SerializeField] private GameObject startGameTarget;

    [SerializeField] private TMP_Text tutorialText;
    
    // Start is called before the first frame update
    void Start()
    {
        holsterRight.selectExited.AddListener(PlayerDrawsWeapon);
        holsterLeft.selectExited.AddListener(PlayerDrawsWeapon);

        tutorialText.text =
            $"Welcome to Western VR Shooter! \n Start by grabbing either pistols from your holsters (using the grab button)";
    }

    private void PlayerDrawsWeapon(SelectExitEventArgs arg0)
    {
        // complete this phase
        
        holsterRight.selectExited.RemoveListener(PlayerDrawsWeapon);
        holsterLeft.selectExited.RemoveListener(PlayerDrawsWeapon);
        
        gun1.activated.AddListener(PlayerFiresWeapon);
        gun2.activated.AddListener(PlayerFiresWeapon);
        
        tutorialText.text =
            $"Now use the trigger to fire your Weapon.";
        
    }

    private void PlayerFiresWeapon(ActivateEventArgs arg0)
    {
        gun1.activated.RemoveListener(PlayerFiresWeapon);
        gun2.activated.RemoveListener(PlayerFiresWeapon);
        
        flickDetectorRight.OnFlick.AddListener(PlayerFlicksWeapon);
        flickDetectorLeft.OnFlick.AddListener(PlayerFlicksWeapon);
        
        tutorialText.text =
            $"Its time to Reload! \n Open your pistol drum by flicking your hand horizontally fast.";
    }

    private void PlayerFlicksWeapon()
    {
        flickDetectorRight.OnFlick.RemoveListener(PlayerFlicksWeapon);
        flickDetectorLeft.OnFlick.RemoveListener(PlayerFlicksWeapon);
        
        handEquipRight.OnReload.AddListener(PlayerReloadsWeapon);
        handEquipLeft.OnReload.AddListener(PlayerReloadsWeapon);
        
        tutorialText.text =
            $"With the drum open, move the pistol to your holster to reload the bullets. \n You can flick horizontally to close the drums and fire again.";
    }

    private void PlayerReloadsWeapon()
    {
        handEquipRight.OnReload.RemoveListener(PlayerReloadsWeapon);
        handEquipLeft.OnReload.RemoveListener(PlayerReloadsWeapon);
        startGameTarget.SetActive(true);

        tutorialText.text = $"That's it! \n Shoot the Barrel to start";
    }
    
}
