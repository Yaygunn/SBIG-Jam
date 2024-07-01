using UnityEngine;

namespace Components.Carriables.Original
{
    public class Carriable : BaseCarriable
    {
        [field: SerializeField] public string Name { get; protected set; } = "****";
        [field: SerializeField] protected Vector3 CarryPosition { get; private set; }
        [field: SerializeField] protected float ScaleDown { get; private set; } = 1;

        protected Vector3 StartScale;

        public override BaseCarriable PickUp(Transform camera)
        {
            CloseCollider();

            StartScale = transform.localScale;

            transform.parent = camera;
            transform.localPosition = CarryPosition;
            transform.localRotation = Quaternion.identity;
            transform.localScale *= ScaleDown;

            return this;
        }
        public override void Drop()
        {
            transform.SetParent(null);
            transform.localScale = StartScale;

            OpenCollider();
        }

        public override string GetUiText()
        {
            return Name;
        }

        private void OpenCollider()
        {
            GetComponent<Collider>().enabled = true;
        }
        private void CloseCollider()
        {
            GetComponent<Collider>().enabled = false;
        }
    }
}