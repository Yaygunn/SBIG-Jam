using System;
using System.Collections;
using System.Collections.Generic;
using Components.BulletHit;
using Controller.Enemy.States;
using Enums;
using Enums.Golem;
using Manager.Enemy;
using Managers.Global;
using Scriptables.Enemy;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Controller.Enemy
{
    public class EnemyController : MonoBehaviour, IWaterHit, IGolemHit, ISlapHit, IBasketBallHit, IRockHit
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
        public EatCropState StateEatCrop { get; private set; }
        public EnterGardenState StateEnterGarden { get; private set; }
        public LeaveGardenState StateLeaveGarden { get; private set; }
        public KnockedBackState StateKnockedBack { get; private set; }
        public SlapState StateSlap { get; private set; }
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
        
        public static int EnemyCount { get; private set; }

        private void Awake()
        {
            EnemyCount++;
        }
        
        private void Start()
        {
            NavMeshAgent = GetComponent<NavMeshAgent>();
            GolemAnimator = GetComponent<Animator>();
            RB = GetComponent<Rigidbody>();
            
            // Ensure we don't affect the Enemy movement
            RB.isKinematic = true;
        }

        private void OnDestroy()
        {
            EnemyCount--;
            
            EventHub.Ev_SlapHit -= TriggerSlapSequence;
            EventHub.Ev_EnemyEndAndFlee -= TriggerFleeSequence;
            EventHub.Ev_EnemyEndAndKill -= TriggerDeathSequence;
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
            StateSlap = new SlapState(this);
            StateEatCrop = new EatCropState(this);
            
            // Spawned enemies will default to EnterGardenState
            StateCurrent = StateEnterGarden;
            StateCurrent?.Enter();

            EventHub.Ev_SlapHit += TriggerSlapSequence;
            EventHub.Ev_EnemyEndAndFlee += TriggerFleeSequence;
            EventHub.Ev_EnemyEndAndKill += TriggerDeathSequence;

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

        public void TriggerSlapSequence()
        {
            ChangeState(StateSlap);
        }
        
        private void TriggerFleeSequence()
        {
            ChangeState(StateLeaveGarden);
        }
        
        private void TriggerDeathSequence()
        {
            EnemyManager.Instance.SpawnMiniEnemy(EnemyConfig.golemType, transform.position);
            
            Destroy(gameObject);
        }

        private void SetHealth(int amount)
        {
            Health = amount;
        }

        public void TakeDamage(int amount, bool allowKilling = true)
        {
            Health -= amount;

            if (Health <= 0 && !allowKilling)
            {
                Health = 1;
            }
            
            if (Health <= 0 && allowKilling)
            {
                EnemyManager.Instance.SpawnMiniEnemy(EnemyConfig.golemType, transform.position);
            
                Destroy(gameObject);
                
                return;
            }
            
            StartCoroutine(ShowVisualDamage());
        }
        
        private IEnumerator ShowVisualDamage()
        {
            MeshRenderer.material.SetColor("_Color", _damageColors[Random.Range(0, _damageColors.Count)]);
            yield return new WaitForSeconds(0.1f);
            MeshRenderer.material.SetColor("_Color", Color.white);
        }

        public IEnumerator DestroyAfterTime(float f)
        {
            yield return new WaitForSeconds(f);
            
            Destroy(gameObject);
        }
        
        #region On Hit Effects
        public void OnSlapHit()
        {
            EventHub.SlapHit();
        }
        
        public void OnGolemHit(int damageAmount, Vector3 direction, Vector3 spawnPoint)
        {
            hitDirection = direction;
            TakeDamage(damageAmount);
            ChangeState(StateKnockedBack);
            
        }
        
        public void OnWaterHit(int damageAmount)
        {
            if (EnemyConfig.golemType == EGolemType.WOOD || EnemyConfig.golemType == EGolemType.WATER)
            {
                transform.localScale += new Vector3(0.5f, 0.5f, 0.5f);
                
                TakeDamage(damageAmount);
            }
            else if (EnemyConfig.golemType == EGolemType.FIRE)
            {
                TakeDamage(damageAmount * 2);
            }
            else if (EnemyConfig.golemType == EGolemType.ROCK)
            {
                SetFaceState(EGolemState.HUNGRY);
            }
        }
        
        public void OnBasketBallHit(int damageAmount, Vector3 direction)
        {
            hitDirection = direction;
            TakeDamage(damageAmount, false);
            ChangeState(StateKnockedBack);
        }

        public void OnRockHit(int damageAmount)
        {
            TakeDamage(damageAmount);
        }
        #endregion
    }   
}
