using Managers.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scriptables.Turn.Combat
{
    public class BaseCombatTurn : BaseTurn
    {
        public override void Enter(Action endAction)
        {
            base.Enter(endAction);
            EventHub.CombatStart();
            //UIManager.Instance.ShowCombatUI();
        }

        protected override void EndTurn()
        {
            //UIManager.Instance.HideCombatUI();
            base.EndTurn();
        }
    }
}
