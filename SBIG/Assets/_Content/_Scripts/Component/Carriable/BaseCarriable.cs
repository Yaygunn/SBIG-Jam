using UnityEngine;

namespace Components.Carriables
{
    public abstract class BaseCarriable : MonoBehaviour
    {


        [field: SerializeField] public virtual bool IsPickable { get; protected set; } = true;
        public virtual string GetUiText() { return "____"; }
        public abstract BaseCarriable PickUp(Transform camera);
        public virtual bool Drop(Transform camera) { return false; }
        public virtual void CarryUpdate() { }
    }
}