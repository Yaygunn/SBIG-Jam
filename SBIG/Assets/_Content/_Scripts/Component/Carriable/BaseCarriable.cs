using UnityEngine;

namespace Components.Carriables
{
    public abstract class BaseCarriable : MonoBehaviour
    {
        [field: SerializeField] public bool IsPickable { get; protected set; } = true;
        public virtual string GetUiText() { return "____"; }
        public abstract BaseCarriable PickUp(Transform camera);
        public virtual void Drop() { }
        public virtual void CarryUpdate() { }
    }
}