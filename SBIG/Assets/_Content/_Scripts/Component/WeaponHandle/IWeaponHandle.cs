using UnityEngine;

namespace Components.WeaponHandles
{
    public interface IWeaponHandle
    {
        public void Fire();
        public void Reload();
        public void HideWeapon();
        public void EquipWeapon();

    }
}