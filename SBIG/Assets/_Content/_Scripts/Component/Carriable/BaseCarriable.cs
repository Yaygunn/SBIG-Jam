using UnityEngine;

namespace Components.Carriable
{
    public class BaseCarriable : MonoBehaviour
    {
        [field:SerializeField] public Vector3 CarryPosition { get; private set; }
        public bool IsBeingCarried { get; private set; }
        public virtual void PickUp() { }
        public virtual void Drop() { }
        public virtual void CarryUpdate() { }
    }
}