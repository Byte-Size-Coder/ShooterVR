using BSC.SVR.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class HandEquip : MonoBehaviour
{
    public Gun currentEquippedGun = null;

    public InputActionReference primaryButtonReference;

    public string orientation;

    private bool canReload;
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
        Debug.Log("I GRABBED A GUN!");
    }

    public void UnEquip()
    {
        if (currentEquippedGun == null ) return;


        currentEquippedGun = null;
    }

    private void Reload()
    {
        if (currentEquippedGun == null) return;
      
        currentEquippedGun.Reload();
        
    }

    private void ToggleDrum(InputAction.CallbackContext context)
    {
        Debug.Log("TEST! TESSSt");
        Debug.Log(currentEquippedGun);
        if (currentEquippedGun == null) return;

        currentEquippedGun.ToggleDrum(orientation);
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "Reload")
        {
            Debug.Log("FOUND RELOAD");
            Holster holster = other.GetComponent<Holster>();
            if (holster)
            {
                Reload();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {

    }
}
