using UnityEngine;

namespace Scriptables.Magazines.Child.BubbleMag
{
    [CreateAssetMenu(fileName = "BubbleMag", menuName = "Scriptables/Magazines/Bubble")]
    public class BubbleMagazine : BaseMagazine
    {
        protected override void OnButtonPressed()
        {
            base.OnButtonPressed();
            Debug.Log("Bubble Fire");
            EndMagazine();
        }
    }
}