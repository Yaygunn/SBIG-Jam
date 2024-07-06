using UnityEngine;

namespace Components.Dependency.Player
{
    public class Dependencies : MonoBehaviour
    {
        [field:SerializeField] public Camera FpsCam {  get; private set; }
        [field:SerializeField] public HeadBob Headbob {  get; private set; }
    }
}
