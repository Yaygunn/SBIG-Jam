using Components.Carriables;
using Components.Dependency.Player;
using DataSingleton.Layer;
using UnityEngine;

namespace Components.Carry.Original
{
    public class CarryComp : MonoBehaviour, ICarryComp
    {
        private Dependencies _dependencies;

        private float _rayDistance = 2.6f;

        private BaseCarriable _carriableInFrontCamera;

        private BaseCarriable _carriableBeingCarried;

        private void OnEnable()
        {
            _dependencies = GetComponent<Dependencies>();
        }
        public void LogicUpdate()
        {
            RayCastCalculation();

            if(_carriableBeingCarried != null)
                _carriableBeingCarried.CarryUpdate();
        }

        public void OnActivateCauldronButton()
        {
            // try to cook with cauldron
            print("cook");
            
        }

        public void OnInteractButton()
        {
            if (_carriableInFrontCamera != null)
            {
                if (_carriableBeingCarried != null)
                    Drop();

                Pick();
            }
            else if(_carriableBeingCarried != null)
            {
                Drop();
            }
            
        }

        private void Pick()
        {
            _carriableInFrontCamera.PickUp(_dependencies.FpsCam.transform);
            _carriableBeingCarried = _carriableInFrontCamera;
            _carriableInFrontCamera = null;
        }

        private void Drop()
        {
            _carriableBeingCarried.Drop();
            _carriableBeingCarried = null;
        }

        private void RayCastCalculation()
        {
            if (Physics.Raycast(_dependencies.FpsCam.transform.position, _dependencies.FpsCam.transform.forward, out RaycastHit hit, _rayDistance, Layers.Instance.CarryLayer))
            {
                _carriableInFrontCamera = hit.collider.GetComponent<BaseCarriable>();
            }
            else
            {
                _carriableInFrontCamera = null;
            }
        }

    }
}
