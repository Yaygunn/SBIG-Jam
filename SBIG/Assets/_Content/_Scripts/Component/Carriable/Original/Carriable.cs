using UnityEngine;

namespace Components.Carriables.Original
{
    public class Carriable : BaseCarriable
    {
        [field: SerializeField] protected Vector3 CarryPosition { get; private set; }
        [field: SerializeField] protected float ScaleDown { get; private set; } = 1;

        protected Vector3 StartScale;

        public override void PickUp(Transform camera)
        {
            base.PickUp(camera);

            CloseCollider();

            StartScale = transform.localScale;

            transform.parent = camera;
            transform.localPosition = CarryPosition;
            transform.localRotation = Quaternion.identity;
            transform.localScale *= ScaleDown;
        }
        public override void Drop()
        {
            base.Drop();

            transform.SetParent(null);
            transform.localScale = StartScale;

            OpenCollider();
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