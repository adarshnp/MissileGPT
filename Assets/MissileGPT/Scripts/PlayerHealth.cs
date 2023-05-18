using System;
using UnityEngine;

namespace missilegpt
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField]
        public AgentDataScriptableBase playerData;

        private float currenthealth;

        public static event Action<float, float, float, float> onScoreUpdate;
        public static event Action onPlayerDeath;
        private void OnEnable()
        {
            GameManager.onGameRestart += ResetHealth;
        }
        private void Start()
        {
            //event subscription when put in onEnable cause null ref
            MissileExplosion.explosion.onExplosionEvent += TakeDamage;

            currenthealth = playerData.health;
        }
        void Die()
        {
            onPlayerDeath?.Invoke();
            Time.timeScale = 0;
        }
        void TakeDamage(ExplosionData explosionData)
        {
            MissileDataScriptableBase data = explosionData.data;
            float distance = Vector3.Distance(transform.position, explosionData.hitPoint);
            if (distance == 0)
            {
                Die();
                return;
            }

            float damageMultiplier = Mathf.Clamp01((data.attackRadius - distance) / data.attackRadius);
            float damage = data.maxDamage * Mathf.Abs(damageMultiplier);
            currenthealth = currenthealth - damage;
            onScoreUpdate?.Invoke(distance, data.attackRadius, damage, currenthealth);
            //Debug.Log($"Distance {distance}!!! DAMAGE {damage}!!! HEALTH {currenthealth}");
            if (currenthealth <= 0)
            {
                Die();
            }
        }
        private void OnDisable()
        {
            MissileExplosion.explosion.onExplosionEvent -= TakeDamage;
            GameManager.onGameRestart -= ResetHealth;
        }
        private void ResetHealth()
        {
            currenthealth = playerData.health;
        }
    }
}