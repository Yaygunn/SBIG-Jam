using Components.Weapons;
using FMODUnity;
using System;
using UnityEngine;
using YInput;

namespace Scriptables.Magazines
{
    public class BaseMagazine : ScriptableObject
    {
        [field:SerializeField] public string MagName { get; private set; }
        [field: SerializeField] public Material MagMaterial;
        [field: SerializeField] public EventReference FireSoundEvent {  get; private set; }
        protected bool IsMagazineOver;
        protected Action EndAction { get; private set; }
        protected BaseWeapon Weapon { get; private set; }

        [SerializeField] private int _maxAmmo = 5;
        private int _currentAmmo;
        public bool LimitlessAmmo { get; private set; }
        public virtual void Enter(Action endAction, BaseWeapon weapon)
        {
            EndAction = endAction;
            Weapon = weapon;
            IsMagazineOver = false;
            _currentAmmo = _maxAmmo;
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

        protected void ReduceAmmo()
        {
            if (LimitlessAmmo)
                return;

            _currentAmmo--;
            if(_currentAmmo <= 0)
                EndMagazine();
        }
        protected GameObject InstantiateBullet(GameObject prefab)
        {
            return Instantiate(prefab, new Vector3 (-20,-20,-20), Quaternion.identity);
        }
        
    }
}