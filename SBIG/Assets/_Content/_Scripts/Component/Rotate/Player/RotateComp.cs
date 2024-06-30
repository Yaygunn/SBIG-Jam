using Components.Dependency.Player;
using UnityEngine;

namespace Components.Rotate.Player
{
    public class RotateComp : MonoBehaviour, IRotateComponent
    {
        private Dependencies _dependencies;

        readonly private float _maxY = 80;
        readonly private float _minY = -60;
        private float _currentY;
        private void OnEnable()
        {
            _dependencies = GetComponent<Dependencies>();
        }
        public void Rotate(Vector2 rotateDirection)
        {
            //change here
            _currentY = Mathf.Clamp(_currentY + rotateDirection.y, _minY, _maxY);
            _dependencies.FpsCam.transform.localRotation = Quaternion.Euler(-_currentY, 0, 0);

            transform.rotation = transform.rotation * Quaternion.Euler(0, rotateDirection.x, 0);
        }
    }
}
