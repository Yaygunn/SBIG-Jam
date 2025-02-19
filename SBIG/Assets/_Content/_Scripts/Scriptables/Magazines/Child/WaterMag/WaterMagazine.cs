using UnityEngine;

namespace Scriptables.Magazines.Child.WaterMag
{
    [CreateAssetMenu(fileName = "WaterMag", menuName = "Scriptables/Magazines/Water")]
    public class WaterMagazine : BaseMagazine
    {
        [SerializeField] private GameObject _waterBullet;
        protected override void OnButtonPressed()
        {
            base.OnButtonPressed();
            Weapon.Fire(InstantiateBullet(_waterBullet));
            ReduceAmmo();
        }
    }
}