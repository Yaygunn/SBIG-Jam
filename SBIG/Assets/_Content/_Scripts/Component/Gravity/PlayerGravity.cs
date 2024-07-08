using UnityEngine;


namespace Components.Gravity
{
    public class PlayerGravity : MonoBehaviour
    {
        readonly private float _gravity = -9.81f;
        readonly private float _groundCheckDistance = 0.1f;
        [SerializeField] LayerMask _groundMask;

        private CharacterController _controller;
        private Vector3 _velocity;
        private bool _isGrounded;

        void Start()
        {
            _controller = GetComponent<CharacterController>();
        }

        void Update()
        {
            GroundCheck();

            if (_isGrounded && _velocity.y < 0)
            {
                _velocity.y = -2f; // Small negative value to keep the character grounded
            }

            _velocity.y += _gravity * Time.deltaTime;
            _controller.Move(_velocity * Time.deltaTime);
        }

        void GroundCheck()
        {
            Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - _controller.height / 2 + _controller.radius, transform.position.z);
            _isGrounded = Physics.CheckSphere(spherePosition, _groundCheckDistance, _groundMask);
        }

    }
}