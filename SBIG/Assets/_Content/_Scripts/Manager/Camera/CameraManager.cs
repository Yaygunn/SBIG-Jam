using Utilities.Singleton;
using UnityEngine;
using Cinemachine;

namespace Managers.MainCamera
{
    public class CameraManager : Singleton<CameraManager>
    {
        public Camera MainCamera { get; private set; }
        public CinemachineVirtualCamera VirtualCamera { get; private set; }

        #region Singleton
        protected override void Awake()
        {
            base.Awake();

            if (Instance == this)
            {
                Initialize();
            }
        }
        #endregion

        private void Initialize()
        {
            MainCamera = Camera.main;
            VirtualCamera = FindObjectOfType<CinemachineVirtualCamera>();

        }
    }
}