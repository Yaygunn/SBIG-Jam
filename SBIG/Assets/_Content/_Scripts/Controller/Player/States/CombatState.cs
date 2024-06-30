using UnityEngine;
using YInput;

namespace Controller.Player
{
    public class CombatState : ActiveState
    {
        public CombatState(PlayerController player) : base(player) { }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            FireOp();
            Move();
            Rotate();
        }

    }
}
