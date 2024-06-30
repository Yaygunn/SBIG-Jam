using UnityEngine;

namespace Components.Weapons
{
    public class BaseWeapon : MonoBehaviour
    {
        public virtual void Fire() { }
        public virtual void Reload() { }
    }
}