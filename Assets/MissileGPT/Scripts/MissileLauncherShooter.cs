using System.Collections;
using UnityEngine;

namespace missilegpt
{
    public class MissileLauncherShooter : MonoBehaviour
    {
        public PlayerDetection playerDetector;
        public GameObject missilePrefab;
        public Transform gunPoint;
        public Transform missilesParent;
        public Transform MissileLauncherHeadDirection;
        [SerializeField]
        private float MissileLauncherCooldown = 3;
        bool canShoot = false;
        private void OnEnable()
        {
            playerDetector.OnPlayerDetected += StartShoot;
            playerDetector.OnPlayerTrackingLost += StopShoot;
            GameManager.onGameRestart += ClearMissiles;
        }
        void StartShoot() { canShoot = true; }
        void StopShoot() { canShoot = false; }
        float counter = 0;
        void Update()
        {
            if (canShoot)
            {
                if (counter < MissileLauncherCooldown)
                {
                    counter += Time.deltaTime;
                }
                else
                {
                    LaunchMissile();
                    counter = 0;
                }
            }
        }
        void LaunchMissile()
        {
            MissileLauncherHeadDirection.LookAt(playerDetector.player);
            StartCoroutine(Shoot());
        }
        IEnumerator Shoot()
        {
            yield return new WaitForSecondsRealtime(MissileLauncherCooldown);
            GameObject missile = Instantiate(missilePrefab, gunPoint, false);
            yield return new WaitForFixedUpdate();
            missile.transform.parent = missilesParent;
        }
        private void OnDisable()
        {
            playerDetector.OnPlayerDetected -= StartShoot;
            playerDetector.OnPlayerTrackingLost -= StopShoot;
            GameManager.onGameRestart -= ClearMissiles;
        }
        void ClearMissiles()
        {
            for (int i = missilesParent.childCount - 1; i >= 0; i--)
            {
                Destroy(missilesParent.GetChild(i).gameObject);
            }
        }
    }
}