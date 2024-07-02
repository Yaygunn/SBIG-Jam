using UnityEngine;
using Cinemachine;
using Managers.MainCamera;

namespace Components.Cameras.HeadBob
{
    public class HeadBobComp : MonoBehaviour
    {
        [SerializeField]
        private float _defaultBobFrequency = 0.5f;
        [SerializeField]
        private float _defaultBobAmplitude = 1.0f;
        [SerializeField]
        private float _maxBobFrequency = 1.0f;
        [SerializeField]
        private float _maxBobAmplitude = 2.0f;

        [SerializeField]
        private float _targetAmplitude = 0;

        [SerializeField]
        private float _targetFrequency = 0;
        [SerializeField]
        private float _maxSpeed = 5.0f;

        private CharacterController _charController;

        [SerializeField]
        private float _interpolationSpeed = 5.0f;
        private Vector3 _originalPosition;
        private CinemachineBasicMultiChannelPerlin _noise;

        void Start()
        {
            _charController = GetComponentInParent<CharacterController>();
            _noise = CameraManager.Instance.VirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

            if (_noise == null)
            {
                _noise = CameraManager.Instance.VirtualCamera.AddCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            }

            _originalPosition = CameraManager.Instance.VirtualCamera.transform.localPosition;
        }

        void Update()
        {
            HandleHeadBob();
        }

        void HandleHeadBob()
        {
            float speed = _charController.velocity.magnitude;

            if (speed > 0.1f)
            {
                _targetAmplitude = Mathf.Lerp(_defaultBobAmplitude, _maxBobAmplitude, speed / _maxSpeed);
                _targetFrequency = Mathf.Lerp(_defaultBobFrequency, _maxBobFrequency, speed / _maxSpeed);
            }
            else
            {
                _targetAmplitude = _defaultBobAmplitude;
                _targetFrequency = _defaultBobFrequency;
                CameraManager.Instance.VirtualCamera.transform.SetLocalPositionAndRotation(Vector3.Lerp(CameraManager.Instance.VirtualCamera.transform.localPosition, _originalPosition, Time.deltaTime * _interpolationSpeed), Quaternion.Lerp(CameraManager.Instance.VirtualCamera.transform.localRotation, Quaternion.identity, Time.deltaTime * _interpolationSpeed));
            }

            _noise.m_AmplitudeGain = Mathf.Lerp(_noise.m_AmplitudeGain, _targetAmplitude, Time.deltaTime * _interpolationSpeed);
            _noise.m_FrequencyGain = Mathf.Lerp(_noise.m_FrequencyGain, _targetFrequency, Time.deltaTime * _interpolationSpeed);
        }
    }
}