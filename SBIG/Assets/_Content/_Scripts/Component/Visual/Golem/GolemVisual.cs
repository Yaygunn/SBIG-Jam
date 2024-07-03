using UnityEngine;

namespace Components.Visual.Golem
{
    public enum EGolemFaces { angry, dizzy, happy, hungry}
    public class GolemVisual : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private Animator _animator;
        [SerializeField] private Renderer _renderer;

        [Header("Faces")]
        [SerializeField] private Material _happyMaterial;
        // add the remainings here please

        [Header("Test")]
        [SerializeField] private bool headBut;
        [SerializeField] private float _speed;
        [SerializeField] private bool _charge;

        private string _headButTrigger = "HeadBut";
        private string _chargeBool = "Charge";
        private string _speedFloat = "Speed";

        private void Update()
        {

            SetSpeed(_speed);

            if (_charge)
            {
                Charge();
            }
            else
            {
                EndCharge();
            }

            if(headBut)
            {
                headBut = false;
                HeadBut();
            }
        }

        public void HeadBut()
        {
            _animator.SetTrigger(_headButTrigger);
        }

        public void Charge()
        {
            _animator.SetBool(_chargeBool, true);
        }

        public void EndCharge()
        {
            _animator.SetBool(_chargeBool, false);
        }
        public void SetSpeed(float speed)
        {
            _animator.SetFloat(_speedFloat, speed);
        }
        public void ChangeFace(EGolemFaces face)
        {
            switch(face)
            {
                case EGolemFaces.happy:
                    _renderer.material = _happyMaterial;
                    break;
                // add remaining faces please
            }
        }
    }
}
