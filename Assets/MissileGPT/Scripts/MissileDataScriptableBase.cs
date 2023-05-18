using UnityEngine;
namespace missilegpt
{
    [CreateAssetMenu(fileName = "Bullet", menuName = "ScriptableObjects/BulletTypes")]
    public class MissileDataScriptableBase : ScriptableObject
    {
        public float attackRadius;
        public float maxDamage;
    }
}