using UnityEngine;

namespace Components.Move
{
    public interface IMoveComponent
    {
        public void Move(Vector2 moveDirection);
    }
}