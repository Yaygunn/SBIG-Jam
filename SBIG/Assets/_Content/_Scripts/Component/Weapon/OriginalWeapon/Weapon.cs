using Components.BulletMove;
using FMODUnity;
using Managers.Magazines;
using Managers.MainCamera;
using Scriptables.Magazines;
using Manager.Caption;
using UnityEngine;
using UnityEngine.Diagnostics;
using Utilities;
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
        
        #region Narration Ammo Types
        [field: SerializeField] public EventReference FirstBubbleAmmo { get; private set; }
        [field: SerializeField] public EventReference FirstWaterAmmo { get; private set; }
        [field: SerializeField] public EventReference FirstGolemAmmo { get; private set; }
        [field: SerializeField] public EventReference FirstBasketballAmmo { get; private set; }
        [field: SerializeField] public EventReference FirstRockAmmo { get; private set; }
        
        private bool _loadedBubbleOnce;
        private bool _loadedWaterOnce;
        private bool _loadedGolemOnce;
        private bool _loadedBasketballOnce;
        private bool _loadedRockOnce;
        #endregion

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
            EventHub.Fired();
            if (!_currentMagazine.FireSoundEvent.IsNull)
            {
                RuntimeManager.PlayOneShot(_currentMagazine.FireSoundEvent);
            } 
            else
                print("null sound event");

            // This is probably the worst way I could do this, but it works...
            switch (_currentMagazine.MagName)
            {
                case "Basketball":
                    if (!_loadedBasketballOnce)
                    {
                        _loadedBasketballOnce = true;
                        CaptionManager.Instance.StartCaption( SBIGUtils.GetEventName(FirstBasketballAmmo) );
                        RuntimeManager.PlayOneShot(FirstBasketballAmmo);
                    }
                    break;
                case "Bubble":
                    if (!_loadedBubbleOnce)
                    {
                        _loadedBubbleOnce = true;
                        CaptionManager.Instance.StartCaption( SBIGUtils.GetEventName(FirstBubbleAmmo) );
                        RuntimeManager.PlayOneShot(FirstBubbleAmmo);
                    }

                    break;
                case "Golem":
                    if (!_loadedGolemOnce)
                    {
                        _loadedGolemOnce = true;
                        CaptionManager.Instance.StartCaption( SBIGUtils.GetEventName(FirstGolemAmmo) );
                        RuntimeManager.PlayOneShot(FirstGolemAmmo);
                    }

                    break;
                case "Rock":
                    if (!_loadedRockOnce)
                    {
                        _loadedRockOnce = true;
                        CaptionManager.Instance.StartCaption( SBIGUtils.GetEventName(FirstRockAmmo) );
                        RuntimeManager.PlayOneShot(FirstRockAmmo);
                    }

                    break;
                case "Water":
                    if (!_loadedWaterOnce)
                    {
                        _loadedWaterOnce = true;
                        CaptionManager.Instance.StartCaption( SBIGUtils.GetEventName(FirstWaterAmmo) );
                        RuntimeManager.PlayOneShot(FirstWaterAmmo);
                    }

                    break;
            }
        }
    }
}
