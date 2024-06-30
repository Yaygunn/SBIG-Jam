using UnityEngine;
using Utilities.Singleton;

namespace DataSingleton.Layer
{
    public class Layers : Singleton<Layers>
    {
        [field:SerializeField] public LayerMask CarryLayer { get; private set; }
    }
}
