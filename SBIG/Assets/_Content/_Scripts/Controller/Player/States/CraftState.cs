using UnityEngine;
using YInput;

namespace Controller.Player.State
{
    public class CraftState : ActiveState
    {
        public CraftState(PlayerController player) : base(player) { }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            Move();
            Rotate();
            Interaction();
        }


    }
}
