using Enums.CookableType;
using UnityEngine;

namespace Components.Cookables
{
    public class BaseCookable : MonoBehaviour
    {
        [field:SerializeField] public ECookableType type {  get; private set; }

        public virtual void ThrowenToCauldron()
        {
            Destroy(gameObject);
        }
    }
}
