using Components.BulletMove;
using Managers.Magazines;
using Scriptables.Magazines;
using UnityEngine;
using YInput;

namespace Components.Weapons.Original
{
    public class Weapon : BaseWeapon
    {
        private BaseMagazine _currentMagazine;

        private void Start()
        {
            EventHub.Ev_ForceEndMagazine += ForceEndMagazine;
        }
        private void OnDestroy()
        {
            EventHub.Ev_ForceEndMagazine -= ForceEndMagazine;
        }
        public override void SendFireInput(InputState inputState)
        {
            _currentMagazine?.LogicTick(inputState);
        }

        public override void Reload()
        {
            EquipNewMagazine(ReloadManager.Instance.GetNewMagazine());
        }

        private void EquipCauldronMagazine(BaseMagazine magazine)
        {
            _currentMagazine.InstanceChangeToNewMagazine();
            _currentMagazine = null;//get cauldron magazine
            EquipNewMagazine(_currentMagazine);
        }

        private void EquipNewMagazine(BaseMagazine magazine)
        {
            _currentMagazine = magazine;
            _currentMagazine.Enter(OnMagazineEnd, this);
        }

        private void OnMagazineEnd()
        {
            print("MagazineEnded");
            _currentMagazine = null;
            EventHub.MagazineEnded();
        }

        private void ForceEndMagazine()
        {
            _currentMagazine?.EndMagazine();
        }

        public override void Fire(GameObject bullet)
        {
            print("fire " + bullet.name);
            bullet.transform.position = FireLocation.transform.position;
            bullet.transform.rotation = FireLocation.transform.rotation;
            bullet.GetComponent<IBulletMove>().Initialize();
        }
    }
}
