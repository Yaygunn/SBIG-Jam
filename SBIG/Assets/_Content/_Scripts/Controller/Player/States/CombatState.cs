using UnityEngine;
using YInput;

namespace Controller.Player.State
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
