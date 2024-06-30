using Components.Move;
using Components.Rotate;
using Components.WeaponHandles;
using Managers.SceneChange;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controller.Player
{
    public class PlayerController : MonoBehaviour
    {
        #region States
        public CombatState StateCombat {  get; private set; }  
        #endregion

        #region Components
        public IMoveComponent CompMove { get; private set; }
        public IRotateComponent CompRotate { get; private set; }
        public IWeaponHandle CompWeaponHandle { get; private set; }
        #endregion

        public BaseState StateCurrent { get; private set; }

        private void Start()
        {
            #region Instantiate States 
            StateCombat = new CombatState(this);
            #endregion

            #region Get Components
            CompMove = GetComponent<IMoveComponent>();
            CompRotate = GetComponent<IRotateComponent>();
            CompWeaponHandle = GetComponent<IWeaponHandle>();
            #endregion

            StateCurrent = StateCombat;
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
    }
}
