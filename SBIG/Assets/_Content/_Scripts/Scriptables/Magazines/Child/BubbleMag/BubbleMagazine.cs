using UnityEngine;

namespace Scriptables.Magazines.Child.BubbleMag
{
    [CreateAssetMenu(fileName = "BubbleMag", menuName = "Scriptables/Magazines/Bubble")]
    public class BubbleMagazine : BaseMagazine
    {
        [SerializeField] private GameObject _bubbleBullet;
        
        protected override void OnButtonPressed()
        {
            base.OnButtonPressed();
            Weapon.Fire(InstantiateBullet(_bubbleBullet));
            ReduceAmmo();
        }
    }
}