using Scriptables.Turn;
using UnityEngine;
using Utilities.Singleton;

namespace Managers.Turn
{
    public class TurnManager : Singleton<TurnManager> 
    {
        [SerializeField] private TurnHolder[] _turnHolder;

        private int _turnIndex;

        private void Start()
        {
            SwitchTurn();
        }
        private void PlayCoroutine(BaseTurn turn)
        {
            StartCoroutine(turn.TurnOperations());
        }

        private void SwitchTurn()
        {
            if(_turnIndex >= _turnHolder.Length)
            {
                Debug.Log("There are no Turn");
                return;
            }

            _turnHolder[_turnIndex].Enter(SwitchTurn, PlayCoroutine);
            _turnIndex++;
        }
    }

    
}
