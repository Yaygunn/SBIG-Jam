using System.Collections;
using System.Collections.Generic;
using Components.BulletHit;
using Controller.Enemy.States;
using Enums;
using Enums.Golem;
using Managers.Global;
using Scriptables.Enemy;
using UnityEngine;
using UnityEngine.AI;

namespace Controller.Enemy
{
    public class EnemyController : MonoBehaviour, IWaterHit, IGolemHit, ISlapHit, IBasketBallHit
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
        public KnockedBackState StateKnockedBack { get; private set; }
        #endregion
        
        public NavMeshAgent NavMeshAgent;
        public Transform EntrancePoint;
        public SkinnedMeshRenderer MeshRenderer;
        public Animator GolemAnimator;
        public Rigidbody RB;
        
        public Vector3 hitDirection;
        public ETargetType TargetType;
        
        #region Health
        public int Health { get; private set; }
        private List<Color> _damageColors = new List<Color>
        {
            new Color(0.45f, 0.27f, 0.54f),
            new Color(0, 0.67f, 0.73f),
            new Color(0.61f, 0.85f, 0.62f),
            new Color(1, 0.92f, 0.65f),
        };
        #endregion

        private void Start()
        {
            NavMeshAgent = GetComponent<NavMeshAgent>();
            GolemAnimator = GetComponent<Animator>();
            RB = GetComponent<Rigidbody>();
            
            // Ensure we don't affect the Enemy movement
            RB.isKinematic = true;
        }

        public void Initialize(EnemyData data)
        {
            EnemyConfig = data;
            
            StartCoroutine(SetupStates());
        }

        private IEnumerator SetupStates()
        {
            yield return 0;
            
            HandleChangeTexture();
            SetHealth(EnemyConfig.baseHealth);
            
            // wait till next frame
            StateIdle = new IdleState(this);
            StateChase = new ChaseState(this);
            StateCombat = new CombatState(this);
            StateEnterGarden = new EnterGardenState(this);
            StateLeaveGarden = new LeaveGardenState(this);
            StateKnockedBack = new KnockedBackState(this);
            
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

        public void SetFaceState(EGolemState state)
        {
            EnemyConfig.golemState = state;

            HandleChangeTexture();
        }

        public void OnWaterHit()
        {
            Debug.Log("Enemy hit by water!");
            // If you hit Wood or Water golem, it gets bigger (does normal damage)
            // If you hit fire golum, it takes extra damage
            // If you hit rock golem, nothing happens (does no damage)
        }
        
        public void OnBasketBallHit(int damageAmount, Vector3 direction)
        {
            hitDirection = direction;
            TakeDamage(damageAmount);
            ChangeState(StateKnockedBack);
        }
        
        public void OnSlapHit()
        {
            Debug.Log("Enemy hit by slap!");
            
            // Pause actions
            // Look at Player
            // Notify all other golems on level to do the same
            // Wait X time
            // Set state to LeaveGarden
        }
        
        public void OnGolemHit()
        {
            Debug.Log("Enemy hit by golem!");
            
            // Knock backs golum
            // Set state to dizzy
            // Wait for X time
            // Spawn a new golem
        }
        
        private void SetHealth(int amount)
        {
            Health = amount;
        }

        public void TakeDamage(int amount)
        {
            Health -= amount;
            
            if (Health <= 0)
            {
                // TODO: Spawn in a smaller version of the golem
                StartCoroutine(KillAndSpawnDeath());
                return;
            }
            
            StartCoroutine(ShowVisualDamage());
        }
        
        private IEnumerator KillAndSpawnDeath()
        {
            yield return new WaitForSeconds(StateKnockedBack.KnockbackDuration);
            
            // Spawn a mini golem
            Destroy(gameObject);
        }
        
        private IEnumerator ShowVisualDamage()
        {
            MeshRenderer.material.SetColor("_Color", _damageColors[Random.Range(0, _damageColors.Count)]);
            yield return new WaitForSeconds(0.1f);
            MeshRenderer.material.SetColor("_Color", Color.white);
        }
    }   
}
