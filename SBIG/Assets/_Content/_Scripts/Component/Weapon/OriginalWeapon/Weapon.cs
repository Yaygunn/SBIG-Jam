using Components.BulletMove;
using Managers.Magazines;
using Managers.MainCamera;
using Scriptables.Magazines;
using System.Collections;
using UnityEngine;
using YInput;

namespace Components.Weapons.Original
{
    public class Weapon : BaseWeapon
    {
        private BaseMagazine _currentMagazine;
        public BaseMagazine CurrentMagazine => _currentMagazine;

        private bool _isReloading;

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
            if(_isReloading) 
                return;

            _isReloading = true;
            EventHub.ReloadStarted();
            StartCoroutine(ReloadOver());
        }

        private IEnumerator ReloadOver()
        {
            float reloadTime = 3;

            yield return new WaitForSeconds(reloadTime);

            EquipNewMagazine(ReloadManager.Instance.GetNewMagazine());

            EventHub.ReloadFinished();

            _isReloading = false;
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
            bullet.transform.rotation = CameraManager.Instance.MainCamera.transform.rotation;
            bullet.GetComponent<IBulletMove>().Initialize();
        }
    }
}
