using Controller.Player;
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
            Debug.Log("ActionStart");
            EventHub.CombatStart();
            EventHub.ShowWeapon(true);
            UIManager.Instance.ShowCombatUI();
           
            PlayerController player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            player.ChangeState(player.StateCombat);
        }

        protected override void EndTurn()
        {
            //UIManager.Instance.HideCombatUI();
            EventHub.GrowPlant();
            base.EndTurn();
        }
    }
}
