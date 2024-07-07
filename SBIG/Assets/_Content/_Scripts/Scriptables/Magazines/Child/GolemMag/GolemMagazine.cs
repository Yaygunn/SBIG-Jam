using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scriptables.Magazines.Child.GolemMag
{
    [CreateAssetMenu(fileName = "GolemMag", menuName = "Scriptables/Magazines/Golem")]
    public class GolemMagazine : BaseMagazine
    {
        [SerializeField] private GameObject _golemBullet;
        protected override void OnButtonPressed()
        {
            base.OnButtonPressed();
            Weapon.Fire(InstantiateBullet(_golemBullet));
            ReduceAmmo();
        }
    }   
}
