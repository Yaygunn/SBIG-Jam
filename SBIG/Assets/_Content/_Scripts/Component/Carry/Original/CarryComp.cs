using Components.Carriables;
using Components.Cauldrons;
using Components.Cookables;
using Components.Dependency.Player;
using DataSingleton.Layer;
using UnityEngine;

namespace Components.Carry.Original
{
    public class CarryComp : MonoBehaviour, ICarryComp
    {
        private Dependencies _dependencies;

        private float _rayDistance = 3f;

        private BaseCarriable _carriableInFrontCamera;

        private BaseCarriable _carriableBeingCarried;

        private BaseCookable _cookableBeingCarried;

        private BaseCauldron _cauldronInFront;

        private void OnEnable()
        {
            _dependencies = GetComponent<Dependencies>();
        }
        public void LogicUpdate()
        {
            CloseUiTexts();
            RayCastCalculation();
            OpenUiTexts();

            if (_carriableBeingCarried != null)
                _carriableBeingCarried.CarryUpdate();
        }

        public void OnActivateCauldronButton()
        {
            if (_cauldronInFront != null)
                _cauldronInFront.Cook();
            else
            {
                print("NoCauldronFront");
            }
        }

        public void OnInteractButton()
        {
            if (_cauldronInFront != null)
            {
                if (_cookableBeingCarried != null)
                    ThrowToCauldron();

                else if (_carriableBeingCarried == null)
                    Pick();
            }

            else if (_carriableInFrontCamera != null)
            {
                if (_carriableBeingCarried == null)
                {
                    if(_carriableInFrontCamera.IsPickable)
                        Pick();
                }
            }

            else if (_carriableBeingCarried != null)
            {
                Drop();
            }

        }

        private void Pick()
        {
            if (_cauldronInFront == null)
                EventHub.CropPicked();

            _carriableBeingCarried = _carriableInFrontCamera.PickUp(_dependencies.FpsCam.transform);
            _carriableInFrontCamera = null;

            _cookableBeingCarried = _carriableBeingCarried.GetComponent<BaseCookable>();
            _cauldronInFront = null;
        }

        private void Drop()
        {
            bool _isDropped = _carriableBeingCarried.Drop(_dependencies.FpsCam.transform);

            if (_isDropped)
            {
                _carriableBeingCarried = null;
                _cookableBeingCarried = null;

            }
        }


        private void ThrowToCauldron()
        {
            _cauldronInFront.ThrowInCauldron(_cookableBeingCarried);
            _carriableBeingCarried = null;
            _cookableBeingCarried = null;
        }
        private void RayCastCalculation()
        {
            if (Physics.Raycast(_dependencies.FpsCam.transform.position, _dependencies.FpsCam.transform.forward, out RaycastHit hit, _rayDistance, Layers.Instance.CarryLayer))
            {
                _cauldronInFront = hit.collider.GetComponent<BaseCauldron>();
                _carriableInFrontCamera = hit.collider.GetComponent<BaseCarriable>();
            }
            else
            {
                _cauldronInFront = null;
                _carriableInFrontCamera = null;
            }
        }

        private void OpenUiTexts()
        {
            if (_cauldronInFront != null)
            {
                if (_cookableBeingCarried != null)
                    EventHub.ShowPickableText("throw", true);

                EventHub.ShowCookInCauldronText("throw");
                
            }

            else if (_carriableInFrontCamera != null)
            {
                if(_carriableBeingCarried == null)
                    EventHub.ShowPickableText(_carriableInFrontCamera.GetUiText(), _carriableInFrontCamera.IsPickable);
            }
        }
        public void CloseUiTexts()
        {
            EventHub.CloseCarryTexts();
        }

    }
}
