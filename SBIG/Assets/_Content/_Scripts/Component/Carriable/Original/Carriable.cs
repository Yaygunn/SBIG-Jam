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

        public override bool Drop(Transform camera)
        {
            if (!CanBePlacedOnGround(camera, out Vector3 dropPosition))
            {
                return false;
            }

            transform.SetParent(null);
            transform.localScale = StartScale;

            PlaceOnGround(dropPosition);
            OpenCollider();

            return true;
        }

        private bool CanBePlacedOnGround(Transform camera, out Vector3 dropPosition)
        {
            const float MaxDropDistance = 3f;
            const float RaycastDistance = 7f;
            const float PaddingDistanceFromPlayer = 0.25f;

            dropPosition = Vector3.zero;

            // Check if the item can be placed in front of the player
            if (Physics.Raycast(camera.position, camera.forward, out RaycastHit forwardHit, RaycastDistance))
            {

                dropPosition = forwardHit.distance > MaxDropDistance
                    ? camera.position + camera.forward * MaxDropDistance
                    : forwardHit.point + camera.forward * PaddingDistanceFromPlayer;

                // Check if the item can be placed on the ground
                if (GetRaycastToDownwardSurface(dropPosition, Vector3.down, out RaycastHit downwardHit))
                {
                    dropPosition = downwardHit.point;
                    return true;
                }
            }
            // If the item cannot be placed in front of the player, drop it directly below 
            else
            {

                dropPosition = camera.position + camera.forward * MaxDropDistance;
                if (GetRaycastToDownwardSurface(dropPosition, Vector3.down, out RaycastHit downwardHit))
                {
                    dropPosition = downwardHit.point;
                    return true;
                }
            }

            return false;
        }

        private void PlaceOnGround(Vector3 dropPosition)
        {
            Collider itemCollider = GetComponent<Collider>();
            Bounds itemBounds = itemCollider.bounds;
            dropPosition.y += itemBounds.extents.y + 0.01f;

            transform.SetPositionAndRotation(dropPosition, Quaternion.identity);
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