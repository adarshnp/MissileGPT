using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace missilegpt
{
    public class Missile : MonoBehaviour
    {
        public MissileDataScriptableBase missileData;
        public float lifetime = 5f;
        public float rotationSpeed = 50f;
        public float speed = 100f;

        private Rigidbody rb;
        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            Destroy(gameObject, lifetime);
        }
        private void FixedUpdate()
        {
            rb.velocity = transform.forward * speed;
            RotateMissile();
        }
        void RotateMissile()
        {
            Vector3 targetDirection = rb.velocity.normalized;
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        }
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                MissileExplosion.explosion.SetExplosionData(collision.gameObject.transform.position, missileData, true);
            }
            else /*if (collision.gameObject.layer == LayerMask.NameToLayer("Obstacle"))*/
            {
                MissileExplosion.explosion.SetExplosionData(collision.GetContact(0).point, missileData, false);
            }
            Destroy(gameObject);
        }
    }
}