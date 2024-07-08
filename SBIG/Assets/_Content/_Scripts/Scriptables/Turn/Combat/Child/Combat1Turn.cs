using Controller.Player;
using Manager.Enemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scriptables.Turn.Combat
{
    [CreateAssetMenu(fileName = "Combat1", menuName = "Scriptables/Turn/Combat/Combat1")]
    public class Combat1Turn : BaseCombatTurn
    {
        [HideInInspector] public bool _continue;
        public static Combat1Turn instance { get; private set; }
        public override IEnumerator TurnOperations()
        {
            PlayerController player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            Debug.Log("CombatStart");
            _continue = true;
            instance = this;
            EnemyManager.Instance.ReleaseTheGolems();
            while (_continue)
            {
                player.ChangeState(player.StateCombat);
                yield return null;
            }
            EnemyManager.Instance.EndAndFlee();
            EndTurn();
            Debug.Log("CombatEnd");
            
        }

        
    }
}
