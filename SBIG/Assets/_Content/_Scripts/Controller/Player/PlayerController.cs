using Components.Carry;
using Components.Move;
using Components.Rotate;
using Components.WeaponHandles;
using Controller.Player.State;
using UnityEngine;

namespace Controller.Player
{
    public class PlayerController : MonoBehaviour
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
    }
}