using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scriptables.Magazines.Child.RockMag
{
    [CreateAssetMenu(fileName = "RockMag", menuName = "Scriptables/Magazines/Rock")]
    public class RockMagazine : BaseMagazine
    {
        [SerializeField] private GameObject _rockBullet;
        protected override void OnButtonPressed()
        {
            base.OnButtonPressed();
            Weapon.Fire(InstantiateBullet(_rockBullet));
            EndMagazine();
        }
    }   
}
