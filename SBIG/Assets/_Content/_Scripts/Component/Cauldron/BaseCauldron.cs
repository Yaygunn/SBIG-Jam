using Components.Cookables;
using UnityEngine;

namespace Components.Cauldrons
{
    public class BaseCauldron : MonoBehaviour
    {
        public virtual void ThrowInCauldron(BaseCookable cookable) { }
        public virtual void Cook() { }
    }
}
