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
        public override bool Drop(Transform camera)
        {
            return HandlePlaceOnGround(camera);

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

        private bool HandlePlaceOnGround(Transform camera)
        {
            const float MaxDropDistance = 3f;
            const float RaycastDistance = 7f;
            const float PaddingDistanceFromPlayer = 0.25f;

            Vector3 dropPosition;

            // Check if the item can be placed in front of the player
            if (Physics.Raycast(camera.position, camera.forward, out RaycastHit forwardHit, RaycastDistance))
            {
                Debug.DrawRay(camera.position, camera.forward * forwardHit.distance, Color.red, 300.0f);

                dropPosition = forwardHit.distance > MaxDropDistance
                    ? camera.position + camera.forward * MaxDropDistance
                    : forwardHit.point + camera.forward * PaddingDistanceFromPlayer;
            }
            // If not, place it at the maximum distance
            else
            {
                Debug.DrawRay(camera.position, camera.forward * RaycastDistance, Color.yellow, 300.0f);

                dropPosition = camera.position + camera.forward * MaxDropDistance;
            }

            if (GetRaycastToDownwardSurface(dropPosition, Vector3.down, out RaycastHit downwardHit))
            {
                dropPosition = downwardHit.point;
            }

            Collider itemCollider = GetComponent<Collider>();
            Bounds itemBounds = itemCollider.bounds;
            dropPosition.y += itemBounds.extents.y + 0.01f;

            transform.position = dropPosition;
            transform.SetParent(null);
            transform.localScale = StartScale;
            transform.rotation = Quaternion.identity;

            OpenCollider();

            return true;
        }

        private bool GetRaycastToDownwardSurface(Vector3 position, Vector3 direction, out RaycastHit downwardHit)
        {
            if (Physics.Raycast(position, direction, out downwardHit, 100f))
            {
                return true;
            }

            downwardHit = new RaycastHit();

            return false;
        }
    }
}