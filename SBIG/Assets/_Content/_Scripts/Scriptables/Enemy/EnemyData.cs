using UnityEngine;

namespace Scriptables.Enemy
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptables/Enemy", order = 1)]

    public class EnemyData : ScriptableObject 
    {
        public string enemyName;
        public float detectionRange;
        public float attackRange;
        public bool targetPlayer;
        public bool targetCrops;
        public bool canBeInterrupted;
        public int baseHealth;
        public int baseDamage;
    }
}