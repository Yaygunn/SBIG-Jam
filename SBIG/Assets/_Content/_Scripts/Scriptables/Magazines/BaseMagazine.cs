using Components.Weapons;
using System;
using UnityEngine;
using YInput;

namespace Scriptables.Magazines
{
    public class BaseMagazine : ScriptableObject
    {
        [field:SerializeField] public string MagName { get; private set; }
        protected bool IsMagazineOver;
        protected Action EndAction { get; private set; }
        protected BaseWeapon Weapon { get; private set; }

        public virtual void Enter(Action endAction, BaseWeapon weapon)
        {
            EndAction = endAction;
            Weapon = weapon;
            IsMagazineOver = false;
        }

        public virtual void InstanceChangeToNewMagazine() 
        {
            IsMagazineOver = true;
        }

        public virtual void EndMagazine()
        {
            IsMagazineOver = true;
            EndAction();

        }
        public void LogicTick(InputState inputState) 
        {
            LogicUpdate();

            if(inputState.IsPressed)
                OnButtonPressed();
            if(inputState.IsHeld)
                OnButtonHeld();
            if(inputState.IsReleased)
                OnButtonReleased();
        }

        protected virtual void LogicUpdate() { }
        protected virtual void OnButtonPressed() { }
        protected virtual void OnButtonHeld() { }
        protected virtual void OnButtonReleased() { }

        protected virtual void Fire(GameObject bullet)
        {
            Weapon.Fire(bullet);
        }

        protected GameObject InstantiateBullet(GameObject prefab)
        {
            return Instantiate(prefab, new Vector3 (-20,-20,-20), Quaternion.identity);
        }
        
    }
}