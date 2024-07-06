using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scriptables.Turn.Combat
{
    [CreateAssetMenu(fileName = "Combat1", menuName = "Scriptables/Turn/Combat/Combat1")]
    public class Combat1Turn : BaseCombatTurn
    {
        public override IEnumerator TurnOperations()
        {
            yield return new WaitForSeconds(5);
            EndTurn();
        }
    }
}
