using Scriptables.Turn;
using Scriptables.Turn.Combat;
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

        private void OnEnable()
        {
            EventHub.Ev_Slapped += Slapped;
        }
        private void OnDisable()
        {
            EventHub.Ev_Slapped -= Slapped;

        }
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
            StopAllCoroutines();
            if(_turnIndex >= _turnHolder.Length)
            {
                _turnIndex--;
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

        private void Slapped()
        {
            Invoke("FireEndCombat", 3);
        }
        private void FireEndCombat()
        {
            Combat1Turn.instance._continue = false;
        }
    }

    
}
