using UnityEngine;
using YInput;

namespace Components.Weapon.WeaponSway
{
    public class WeaponSwayComp : MonoBehaviour
    {
        [SerializeField]
        private float _swayAmount = 0.033f;
        [SerializeField]
        private float _smoothAmount = 5.0f;
        [SerializeField]
        private float _swayRotation = 2.5f;
        [SerializeField]
        private float _maxSwayAmount = 0.15f;
        [SerializeField]
        private float _maxSwayRotation = 7.0f;

        private Vector3 _initialPosition;
        private Quaternion _initialRotation;

        void Start()
        {
            _initialPosition = transform.localPosition;
            _initialRotation = transform.localRotation;
        }

        void Update()
        {
            HandleWeaponSway();
        }

        void HandleWeaponSway()
        {
            float horizontalPositionChange = InputHandler.Instance.MousePositionChange.x;
            float verticalPositionChange = InputHandler.Instance.MousePositionChange.y;

            float moveX = Mathf.Clamp(horizontalPositionChange * _swayAmount, -_maxSwayAmount, _maxSwayAmount);
            float moveY = Mathf.Clamp(verticalPositionChange * _swayAmount, -_maxSwayAmount, _maxSwayAmount);

            Vector3 swayPosition = new(moveX, moveY, 0);
            Quaternion swayRotation = Quaternion.Euler(
                Mathf.Clamp(-verticalPositionChange * _swayRotation, -_maxSwayRotation, _maxSwayRotation),
                Mathf.Clamp(horizontalPositionChange * _swayRotation, -_maxSwayRotation, _maxSwayRotation),
                0
            );

            transform.localPosition = Vector3.Lerp(
                transform.localPosition,
                _initialPosition + swayPosition,
                Time.deltaTime * _smoothAmount
            );
            transform.localRotation = Quaternion.Lerp(
                transform.localRotation,
                _initialRotation * swayRotation,
                Time.deltaTime * _smoothAmount
            );
        }
    }
}