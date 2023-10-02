
using BSC.SVR.Score;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace BSC.SVR.Combat
{
    public class Gun : MonoBehaviour
    {
        [Header("Effects")]
        [SerializeField] private Animator anim;
        [SerializeField] private ParticleSystem particle;
        [SerializeField] private Transform rayCastOrigin;
        [SerializeField] private AudioSource gunAudio;
        [SerializeField] private AudioClip fireClip;
        [SerializeField] private AudioClip noFireClip;
        [SerializeField] private AudioClip reloadClip;
        [SerializeField] private AudioClip drumOpenClip;
        [SerializeField] private AudioClip drumCloseClip;

        [Header("Attributes")]
        [SerializeField] private int maxAmmo;

        [SerializeField] private GameObject[] bullets;

        private int currentAmmo;

        public bool drumOpen = false;  

        private void Start()
        {
            currentAmmo = maxAmmo;
        }
        public void Fire()
        {
            if (currentAmmo > 0 && !drumOpen)
            {
                // play anim
                anim.SetTrigger("Fire");
                // play sound
                PlayAudio(fireClip);
                // play particle
                particle.Play();

                RaycastHit hit;

                // raycast
                if (Physics.Raycast(rayCastOrigin.position, rayCastOrigin.forward, out hit, 1000f))
                {
                    Debug.Log("HERE");

                    Debug.Log(hit.transform);

                    Target target = hit.transform.GetComponent<Target>();

                    if (target == null) return;

                    target.OnHit();
                }

                bullets[currentAmmo - 1].SetActive(false);

                currentAmmo--;

                StatTracking.Instance.ShotFired();
            }
            else
            {

                PlayAudio(noFireClip);
                anim.SetTrigger("Fire");
            }
           
        }


        public void Reload ()
        {
            if (!drumOpen) return; 

            currentAmmo = maxAmmo;
            PlayAudio(reloadClip);

            foreach (GameObject bullet in bullets)
            {
                bullet.SetActive(true);
            }
        }

        public void ToggleDrum(string orientation)
        {
            if (orientation == "left")
            {
                if (drumOpen)
                {
                    anim.SetTrigger("CloseRight");
                    drumOpen = false;
                    PlayAudio(drumCloseClip);
                }
                else
                {
                    anim.SetTrigger("OpenRight");
                    drumOpen = true;
                    PlayAudio(drumOpenClip);
                }
            }
            else
            {
                if (drumOpen)
                {
                    anim.SetTrigger("CloseLeft");
                    drumOpen = false;
                    PlayAudio(drumCloseClip);
                }
                else
                {
                    anim.SetTrigger("OpenLeft");
                    drumOpen = true;
                    PlayAudio(drumOpenClip);
                }
            }

            
        }


        private void PlayAudio(AudioClip clip)
        {
            gunAudio.clip = clip;
            gunAudio.Play();
        }
    }
}

