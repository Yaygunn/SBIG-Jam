using UnityEngine;

namespace Components.Carriables
{
    public class BaseCarriable : MonoBehaviour
    {
        public virtual void PickUp(Transform camera) { }
        public virtual void Drop() { }
        public virtual void CarryUpdate() { }
    }
}