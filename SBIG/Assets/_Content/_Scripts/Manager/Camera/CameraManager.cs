using Utilities.Singleton;
using UnityEngine;

namespace Managers.MainCamera
{
    public class CameraManager : Singleton<CameraManager>
    {
        public Camera MainCamera { get; private set; }

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

        }
    }
}