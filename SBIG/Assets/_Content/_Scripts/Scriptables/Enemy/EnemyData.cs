using Enums.Golem;
using FMODUnity;
using UnityEngine;

namespace Scriptables.Enemy
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptables/Enemy", order = 1)]

    public class EnemyData : ScriptableObject 
    {
        public string enemyName;
        public float detectionRange;
        public float attackRange;
        public float attackCooldown;
        public bool targetPlayer;
        public bool targetCrops;
        public bool prioritizePlayer;
        public bool canBeInterrupted;
        public int baseHealth;
        public int baseDamage;
        public EGolemState golemState;
        public EGolemType golemType;
        public EventReference AudioIdle;
        public EventReference AudioCharge;
        public EventReference AudioAttack;
        public EventReference AudioDeath;
    }
}