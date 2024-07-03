using UnityEngine;
using YInput;

namespace Components.Weapons
{
    public class BaseWeapon : MonoBehaviour
    {
        [field:SerializeField] public Transform FireLocation { get; private set; }
        public virtual void SendFireInput(InputState inputState) { }
        public virtual void Reload() { }
        public virtual void Fire(GameObject bullet) { }
    }
}