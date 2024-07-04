using UnityEngine;
using YInput;

namespace Components.WeaponHandles
{
    public interface IWeaponHandle
    {
        public void SendFireInput(InputState input);
        public void Reload();
        public void HideWeapon();
        public void EquipWeapon();

    }
}