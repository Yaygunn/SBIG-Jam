using Managers.MainCamera;
using UnityEngine;

namespace Components.Move.Player
{
    public class MoveComp : MonoBehaviour, IMoveComponent
    {
        [SerializeField] private float speed = 5.0f;

        private CharacterController _charController;

        void Awake()
        {
            _charController = GetComponent<CharacterController>();
        }

        public void Move(Vector2 moveDirection)
        {

            Vector3 direction = new(moveDirection.x, 0, moveDirection.y);
            direction = CameraManager.Instance.MainCamera.transform.TransformDirection(direction);
            direction.y = 0.0f;

            direction.Normalize();

            _charController.Move(speed * Time.deltaTime * direction);
        }
    }
}