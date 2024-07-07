using Scriptables.Turn;
using System.Collections;
using UnityEngine;
using Utilities.Singleton;

namespace Managers.Turn
{
    public class TurnManager : Singleton<TurnManager> 
    {
        [SerializeField] private TurnHolder[] _turnHolder;

        private int _turnIndex;
        bool _isOver;

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
                _isOver = true;
                return;
            }

            _turnHolder[_turnIndex].Enter(SwitchTurn, PlayCoroutine);
            _turnIndex++;
            
            EventHub.TurnChange();
        }

        private void OnDestroy()
        {
            if (_isOver)
                return;

            _turnHolder[_turnIndex-1].DestroyBeforeEnd();
        }
    }

    
}
