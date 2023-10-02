
using BSC.SVR.Game;
using UnityEngine;


namespace BSC.SVR.Combat
{
    public class Gun : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Animator anim;
        [SerializeField] private ParticleSystem particle;
        [SerializeField] private Transform rayCastOrigin;

        [Header("Ammo")]
        [SerializeField] private int maxAmmo;
        [SerializeField] private GameObject[] bullets;

        [Header("Audio")]
        [SerializeField] private AudioSource gunAudio;
        [SerializeField] private AudioClip fireClip;
        [SerializeField] private AudioClip noFireClip;
        [SerializeField] private AudioClip reloadClip;
        [SerializeField] private AudioClip drumOpenClip;
        [SerializeField] private AudioClip drumCloseClip;

        private int currentAmmo;
        private bool drumOpen = false;  

        private void Start()
        {
            currentAmmo = maxAmmo;
        }

        public void Fire()
        {
            if (currentAmmo > 0 && !drumOpen)
            {
                anim.SetTrigger("Fire");
                PlayAudio(fireClip);
                particle.Play();


                bullets[currentAmmo - 1].SetActive(false);

                currentAmmo--;

                StatTracking.Instance.ShotFired();

                RaycastHit hit;

                if (Physics.Raycast(rayCastOrigin.position, rayCastOrigin.forward, out hit, 1000f))
                {
                    Target target = hit.transform.GetComponent<Target>();

                    if (target)
                    {
                        target.OnHit();
                        return;
                    }

                    StartGameTarget startTarget = hit.transform.GetComponent<StartGameTarget>();


                    if (startTarget)
                    {
                        startTarget.OnHit();
                    }

                }
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

        public bool CheckDrumOpen()
        {
            return drumOpen;
        }

        public bool IsAmmoFull()
        {
            return currentAmmo == maxAmmo;
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

