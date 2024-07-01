using Controller.Enemy.States;
using Scriptables.Enemy;
using UnityEngine;
using UnityEngine.AI;

namespace Controller.Enemy
{
    public class EnemyController : MonoBehaviour
    {
        [Header("Enemy Data")]
        [Tooltip("The Scriptable Object that contains the enemy setup and configuration data.")]
        public EnemyData EnemyConfig;
        public Transform Target;
        
        #region States
        public BaseState StateCurrent { get; private set; }
        public IdleState StateIdle { get; private set; }
        public ChaseState StateChase { get; private set; }
        public CombatState StateCombat { get; private set; }
        #endregion
        
        public NavMeshAgent NavMeshAgent;

        private void Start()
        {
            NavMeshAgent = GetComponent<NavMeshAgent>();
            
            StateIdle = new IdleState(this);
            StateChase = new ChaseState(this);
            StateCombat = new CombatState(this);
            
            StateCurrent = StateIdle;
            StateCurrent.Enter();
        }

        private void Update()
        {
            StateCurrent.LogicUpdate();
        }
        
        private void FixedUpdate()
        {
            StateCurrent.PhysicUpdate();
        }
        
        public void ChangeState(BaseState newState)
        {
            if(StateCurrent == newState) 
                return;

            StateCurrent.Exit();
            StateCurrent = newState;
            StateCurrent.Enter();
        }
        
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, EnemyConfig.detectionRange);
            
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, EnemyConfig.attackRange);
        }
    }   
}
