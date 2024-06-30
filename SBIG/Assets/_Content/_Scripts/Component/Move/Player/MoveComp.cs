using UnityEngine;

namespace Components.Move.Player
{
    public class MoveComp : MonoBehaviour, IMoveComponent
    {
        [SerializeField] private float Speed;
        public void Move(Vector2 moveDirection)
        {
            //change here
            transform.position += (transform.forward * moveDirection.y + transform.right * moveDirection.x) * Speed * Time.deltaTime;
        }
    }
}
