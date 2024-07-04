using Components.Weapons;
using UnityEngine;
using YInput;

namespace Components.WeaponHandles
{
    public class WeaponHandle : MonoBehaviour, IWeaponHandle
    {
        private BaseWeapon _weapon;
        private bool _isMagazineEnded = true;

        private void Start()
        {
            _weapon = GetComponentInChildren<BaseWeapon>();

            EventHub.Ev_MagazineEnded += MagazineEnded;
        }
        private void OnDestroy()
        {
            EventHub.Ev_MagazineEnded -= MagazineEnded;
        }
        public void EquipWeapon()
        {
        }

        public void SendFireInput(InputState inputState)
        {
            _weapon.SendFireInput(inputState);
        }

        public void HideWeapon()
        {
            
        }

        public void Reload()
        {
            if (_isMagazineEnded)
            {
                _isMagazineEnded = false;
                _weapon.Reload();
                print("Reload");
            }
            else
            {
                print("Magazine is not over");
            }
        }
        private void MagazineEnded()
        {
            _isMagazineEnded = true;
        }

    }
}
