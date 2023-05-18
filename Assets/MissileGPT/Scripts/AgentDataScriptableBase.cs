using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace missilegpt
{
    [CreateAssetMenu(fileName = "Agent", menuName = "ScriptableObjects/AgentTypes")]
    public class AgentDataScriptableBase : ScriptableObject
    {
        public float health;
        public float movementSpeed;
        public float turnSpeed;
    }
}