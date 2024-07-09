using System;
using Components.BulletHit;
using Components.Carry;
using Components.Move;
using Components.Rotate;
using Components.WeaponHandles;
using Controller.Player.State;
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

        private void Update()
        {
            StateCurrent.LogicUpdate();

            CollisionCooldown();
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