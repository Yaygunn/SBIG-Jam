using UnityEngine;

namespace Components.Rotate
{
    public interface IRotateComponent
    {
        public void Rotate(Vector2 rotateAmount);
    }
}
