using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scriptables.Magazines.Child.SlapMag
{
    [CreateAssetMenu(fileName = "SlapMag", menuName = "Scriptables/Magazines/Slap")]
    public class SlapMagazine : BaseMagazine
    {
        [SerializeField] private GameObject _slapBullet;
        protected override void OnButtonPressed()
        {
            base.OnButtonPressed();
            Weapon.Fire(InstantiateBullet(_slapBullet));
            EndMagazine();
        }
    }
}
