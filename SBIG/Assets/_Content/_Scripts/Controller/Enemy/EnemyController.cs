using System.Collections;
using Controller.Enemy.States;
using Managers.Global;
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
        public EnterGardenState StateEnterGarden { get; private set; }
        public LeaveGardenState StateLeaveGarden { get; private set; }
        #endregion
        
        public NavMeshAgent NavMeshAgent;
        public Transform EntrancePoint;
        public SkinnedMeshRenderer MeshRenderer;

        private void Start()
        {
            NavMeshAgent = GetComponent<NavMeshAgent>();
        }

        public void Initialize(EnemyData data)
        {
            EnemyConfig = data;
            
            HandleChangeTexture();
            StartCoroutine(SetupStates());
        }

        private IEnumerator SetupStates()
        {
            yield return 0;
            
            // wait till next frame
            StateIdle = new IdleState(this);
            StateChase = new ChaseState(this);
            StateCombat = new CombatState(this);
            StateEnterGarden = new EnterGardenState(this);
            StateLeaveGarden = new LeaveGardenState(this);
            
            // Spawned enemies will default to EnterGardenState
            StateCurrent = StateEnterGarden;
            StateCurrent?.Enter();

            yield break;
        }

        private void Update()
        {
            StateCurrent?.LogicUpdate();
        }
        
        private void FixedUpdate()
        {
            StateCurrent?.PhysicUpdate();
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
            if (EnemyConfig == null)
                return;
            
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, EnemyConfig.detectionRange);
            
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, EnemyConfig.attackRange);
        }
        
        public void CalculateClosestEntrance()
        {
            float closestDistance = float.MaxValue;
            int closestIndex = 0;
            
            for (var i = 0; i < GlobalObject.Entrances.Count; i++)
            {
                float distanceToEntrance = Vector3.Distance(transform.position, GlobalObject.Entrances[i].position);

                if (distanceToEntrance < closestDistance)
                {
                    closestDistance = distanceToEntrance;
                    closestIndex = i;
                }
            }
            
            EntrancePoint = GlobalObject.Entrances[closestIndex];
        }
        
        private void HandleChangeTexture()
        {
            string baseTexturePath = $"Art/Golem/{EnemyConfig.golemType.ToString().ToLower()}/{EnemyConfig.golemType.ToString().ToLower()}_{EnemyConfig.golemState.ToString().ToLower()}";

            MeshRenderer.material.SetTexture("_MainTex", Resources.Load<Texture>(baseTexturePath));
        }
    }   
}
