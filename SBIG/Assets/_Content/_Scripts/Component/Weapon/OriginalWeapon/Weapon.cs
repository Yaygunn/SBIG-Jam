using Components.BulletMove;
using FMODUnity;
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
        float reloadTime = 3;
        float _timer;

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
            if(_currentMagazine != null)
            {
                _currentMagazine.LogicTick(inputState);
            }
            else if (inputState.IsPressed)
            {
                EventHub.NoAmmo();
                EventHub.MagazineEnded();
            }
        }

        public override void Reload()
        {
            if(_isReloading) 
                return;

            _isReloading = true;
            EventHub.ReloadStarted();
        }
        private void Update()
        {
            if (_isReloading)
            {
                _timer += Time.deltaTime;
                if(_timer > reloadTime)
                {
                    _timer = 0;
                    EquipNewMagazine(ReloadManager.Instance.GetNewMagazine());

                    EventHub.ReloadFinished();

                    _isReloading = false;
                }
            }
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
            if (!_currentMagazine.FireSoundEvent.IsNull)
                RuntimeManager.PlayOneShot(_currentMagazine.FireSoundEvent);
            else
                print("null sound event");
        }
    }
}
