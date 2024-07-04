using UnityEngine;

namespace Scriptables.Magazines.Child.BasketballMag
{
    [CreateAssetMenu(fileName = "BasketballMag", menuName = "Scriptables/Magazines/Basketball")]
    public class BasketballMagazine : BaseMagazine
    {
        [SerializeField] private GameObject _basketballBullet;
        protected override void OnButtonPressed()
        {
            base.OnButtonPressed();
            Debug.Log("Basketball Fire");
            Weapon.Fire(InstantiateBullet(_basketballBullet));
            EndMagazine();
        }       
    }
}