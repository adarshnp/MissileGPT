using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace missilegpt
{
    public class PlayerDetection : MonoBehaviour
    {
        public float detectionDistance = 100f;
        public float DetectionInterval = 1;

        public LayerMask playerLayer;
        public Transform player;

        public event Action OnPlayerDetected;
        public event Action OnPlayerTrackingLost;


        float counter = 0;
        void DetectPlayer(Transform player)
        {
            Vector3 direction = player.position - transform.position;
            direction.Normalize();
            RaycastHit hit;
            if (Physics.Raycast(transform.position, direction, out hit, detectionDistance, playerLayer))
            {
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
                {
                    OnPlayerDetected?.Invoke();
                }
                else
                {
                    OnPlayerTrackingLost?.Invoke();
                }
            }
        }
        private void Update()
        {
            //tries to detect player on fixed time intervals
            if (counter < DetectionInterval)
                {
                    counter += Time.deltaTime;
                }
                else
                {
                    DetectPlayer(player);
                    counter = 0;
                }
        }
    }
}