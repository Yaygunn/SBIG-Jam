using UnityEngine;
using Components.Dependency.Player;
using System;

namespace Components.Rotate.Player
{
    public class RotateComp : MonoBehaviour, IRotateComponent
    {

        private Dependencies _dependencies;

        readonly private float _maxY = 80;
        readonly private float _minY = -60;
        private float _currentY;

        private Vector2[] _previousInputs = new Vector2[] { Vector2.zero, Vector2.zero, Vector2.zero };

        private int _index;
        private void OnEnable()
        {
            _dependencies = GetComponent<Dependencies>();
        }
        public void Rotate(Vector2 rotateDirection)
        {
            //change here
            rotateDirection = InputSmoot(rotateDirection);
            _currentY = Mathf.Clamp(_currentY + rotateDirection.y, _minY, _maxY);
            _dependencies.FpsCam.transform.localRotation = Quaternion.Euler(-_currentY, 0, 0);

            transform.rotation = transform.rotation * Quaternion.Euler(0, rotateDirection.x, 0);
        }

        private Vector2 InputSmoot(Vector2 input)
        {
            input *= 0.2f;
            _previousInputs[_index] = input;
            _index++;
            _index %= 3;

            Vector2 total = _previousInputs[0] + _previousInputs[1] + _previousInputs[2];
            return total * 0.33f;
        }
    }
}