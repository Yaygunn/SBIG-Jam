using UnityEngine;

namespace Components.Crops
{
    public class BaseCrop : MonoBehaviour
    {
        public virtual void GrowCrop()   {}
        public virtual void Harvest()    {}
        public virtual void DestroyCrop(){}
    }
}

