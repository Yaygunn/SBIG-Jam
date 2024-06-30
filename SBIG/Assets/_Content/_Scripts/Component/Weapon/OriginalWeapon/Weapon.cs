using UnityEngine;

namespace Components.Weapons.Original
{
    public class Weapon : BaseWeapon
    {
        public override void Fire()
        {
            print("fire");
        }

        public override void Reload()
        {
            print("reload");
        }
    }
}
