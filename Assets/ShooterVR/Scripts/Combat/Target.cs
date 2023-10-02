using BSC.SVR.Score;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BSC.SVR.Combat
{
    public class Target : MonoBehaviour
    {

        [Header("Movement")]
        [SerializeField] private float minSpeed;
        [SerializeField] private float maxSpeed;
        [SerializeField] private Vector3 movementDirection;

        [Header("Rotation")]
        [SerializeField] private float rotationSpeedMin;
        [SerializeField] private float rotationSpeedMax;

        [Header("Effects")]
        [SerializeField] private ParticleSystem deathEffect;

      

        private float rotationSpeed;
        private float xAngle, yAngle, zAngle;
        private float moveSpeed;


        // Start is called before the first frame update
        void Start()
        {
            moveSpeed = Random.Range(minSpeed, maxSpeed);
            

            xAngle = Random.Range(0, 360);
            yAngle = Random.Range(0, 360);
            zAngle = Random.Range(0, 360);

            transform.Rotate(xAngle, yAngle, zAngle);

            rotationSpeed = Random.Range(rotationSpeedMin, rotationSpeedMax);
        }

        // Update is called once per frame
        void Update()
        {
            transform.Translate(movementDirection * Time.deltaTime * moveSpeed, Space.World);
            transform.Rotate(Vector3.forward * Time.deltaTime * rotationSpeed);
        }

        public void OnHit()
        {
            // for now

            // play death effect
            GameObject effect = Instantiate(deathEffect.gameObject, transform.position, transform.rotation);
            Destroy(effect, 2.0f);

            StatTracking.Instance.BarrelDestroyed();

            // dead

            Pool.RemoveObject(gameObject);
        }
    }
}
