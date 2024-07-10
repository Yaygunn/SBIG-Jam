using System;
using Components.BulletHit;
using Components.Carry;
using Components.Move;
using Components.Rotate;
using Components.WeaponHandles;
using Controller.Player.State;
using FMOD.Studio;
using FMODUnity;
using Unity.VisualScripting;
using UnityEngine;

namespace Controller.Player
{
    public class PlayerController : MonoBehaviour, IBasketBallHit
    {
        #region States
        public CombatState StateCombat { get; private set; }
        public CraftState StateCraft { get; private set; }
        #endregion

        #region Components
        public CharacterController CharController { get; private set; }
        public IMoveComponent CompMove { get; private set; }
        public IRotateComponent CompRotate { get; private set; }
        public IWeaponHandle CompWeaponHandle { get; private set; }
        public ICarryComp CompCarry { get; private set; }
        #endregion
        
        #region Collision Audio
        [field: SerializeField] public EventReference OnCollisionAudio { get; private set; }
        
        private bool _collisionIsCooldown = false;
        private float _collisionCooldownTimer = 0.5f;
        private float _collisionCooldownDuration = 0.5f;
        #endregion
        
        #region Footsteps Audio
        [field: SerializeField] public EventReference FootstepSound { get; private set; }
        public float WalkingSpeedThreshold = 4.8f;
        private EventInstance _eventInstance;
        #endregion
        
        public int PlayerHealth { get; private set; } = 100;

        public BaseState StateCurrent { get; private set; }

        private void Start()
        {
            #region Instantiate States 
            StateCombat = new CombatState(this);
            StateCraft = new CraftState(this);
            #endregion

            #region Get Components
            CharController = GetComponent<CharacterController>();
            CompMove = GetComponent<IMoveComponent>();
            CompRotate = GetComponent<IRotateComponent>();
            CompWeaponHandle = GetComponent<IWeaponHandle>();
            CompCarry = GetComponent<ICarryComp>();
            #endregion

            StateCurrent = StateCraft;
            StateCurrent.Enter();
        }

        private void OnEnable()
        {
            _eventInstance = RuntimeManager.CreateInstance(FootstepSound); 
            _eventInstance.set3DAttributes(RuntimeUtils.To3DAttributes(transform));
            _eventInstance.start();
        }
        
        private void OnDisable()
        {
            if (_eventInstance.isValid())
            {
                _eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                _eventInstance.release();
            }
        }

        private void Update()
        {
            StateCurrent.LogicUpdate();

            CollisionCooldown();
            HandleFootsteps();
        }

        private void CollisionCooldown()
        {
            if (_collisionIsCooldown)
            {
                _collisionCooldownTimer -= Time.deltaTime;
                if (_collisionCooldownTimer <= 0f)
                {
                    _collisionIsCooldown = false;
                    _collisionCooldownTimer = _collisionCooldownDuration;
                }
            }
        }

        private void HandleFootsteps()
        {
            if (CharController.velocity.magnitude > WalkingSpeedThreshold)
            {
                _eventInstance.setPaused(false);
            }
            else
            {
                _eventInstance.setPaused(true);
            }
        }

        private void FixedUpdate()
        {
            StateCurrent.PhysicUpdate();
        }

        public void ChangeState(BaseState newState)
        {
            if (StateCurrent == newState)
                return;

            StateCurrent.Exit();
            StateCurrent = newState;
            StateCurrent.Enter();
        }
        
        public void TakeDamage(int damage)
        {
            PlayerHealth -= damage;
            EventHub.PlayerHealthChange();
            
            if (PlayerHealth <= 0)
            {
                EventHub.PlayerDied();
            }
        }

        public void OnBasketBallHit(int damageAmount, Vector3 direction)
        {
            TakeDamage(damageAmount);
        }
        
        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            if (_collisionIsCooldown) return;

            Collider collider = hit.collider;
            
            if (collider.gameObject.layer != LayerMask.NameToLayer("Ground"))
            {
                if (!OnCollisionAudio.IsNull)
                {
                    RuntimeManager.PlayOneShot(OnCollisionAudio);
                    _collisionIsCooldown = true;
                }
            }
        }
    }
}