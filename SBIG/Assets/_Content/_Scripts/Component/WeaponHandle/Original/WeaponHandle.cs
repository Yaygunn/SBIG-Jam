using Components.Weapons;
using UnityEngine;

namespace Components.WeaponHandles
{
    public class WeaponHandle : MonoBehaviour, IWeaponHandle
    {
        private BaseWeapon _weapon;
        private void OnEnable()
        {
            _weapon = GetComponentInChildren<BaseWeapon>();
        }
        public void EquipWeapon()
        {
        }

        public void Fire()
        {
            _weapon.Fire();
        }

        public void HideWeapon()
        {
            
        }

        public void Reload()
        {
            _weapon.Reload();
        }

    }
}
