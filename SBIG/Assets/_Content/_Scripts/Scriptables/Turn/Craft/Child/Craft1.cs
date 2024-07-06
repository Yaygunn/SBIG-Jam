using System.Collections;
using UnityEngine;

namespace Scriptables.Turn.Craft.Child
{
    [CreateAssetMenu(fileName = "CraftTest", menuName = "Scriptables/Turn/Craft/Test")]
    public class Craft1 : BaseCraftTurn
    {
        [SerializeField] private string _text;
        public override IEnumerator TurnOperations()
        {
            yield return new WaitForSeconds(5);
            //Debug.Log(_text);
            EndTurn();
        }
    }
}