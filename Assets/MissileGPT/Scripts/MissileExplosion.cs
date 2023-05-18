using System;
using UnityEngine;

namespace missilegpt
{

    public class MissileExplosion : MonoBehaviour
    {
        public GameObject explosionEffectPrefab;

        public event Action<ExplosionData> onExplosionEvent;

        public static MissileExplosion explosion;

        private ExplosionData explosionData = new ExplosionData();

        public MissileDataScriptableBase missileData { get; set; }
        public ExplosionData ExplosionData { get => explosionData; set => explosionData = value; }

        public Transform effectsParent;
        private void Awake()
        {
            if (explosion == null)
            {
                explosion = this;
            }
        }
        private void OnEnable()
        {
            GameManager.onGameRestart += ClearEffects;
        }
        public void SetExplosionData(Vector3 hitPoint, MissileDataScriptableBase missileData, bool hitPlayer)
        {
            explosionData.hitPoint = hitPoint;
            explosionData.data = missileData;
            explosionData.onPlayer = hitPlayer;

            onExplosionEvent?.Invoke(explosionData);
            ExplossionEffect(hitPoint, missileData.attackRadius);
        }
        public void ExplossionEffect(Vector3 hitPoint, float attackRadius)
        {
            GameObject fx = Instantiate(explosionEffectPrefab, effectsParent);
            fx.transform.position = hitPoint;
            fx.GetComponent<AOEDomeEffect>().radius = attackRadius;
        }
        void ClearEffects()
        {
            for (int i = effectsParent.childCount - 1; i >= 0; i--)
            {
                Destroy(effectsParent.GetChild(i).gameObject);
            }
        }
        private void OnDisable()
        {
            GameManager.onGameRestart -= ClearEffects;
        }
    }
    public struct ExplosionData
    {
        public Vector3 hitPoint;
        public MissileDataScriptableBase data;
        public bool onPlayer;
    }


}