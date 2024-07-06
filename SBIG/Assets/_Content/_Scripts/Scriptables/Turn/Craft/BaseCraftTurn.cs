using Managers.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scriptables.Turn.Craft
{
    public class BaseCraftTurn : BaseTurn
    {
        public override void Enter(Action endAction)
        {
            base.Enter(endAction);
            EventHub.CraftStart();
            //UIManager.Instance.ShowCraftUI();
        }

        protected override void EndTurn()
        {
            base.EndTurn();
        }
    }
}