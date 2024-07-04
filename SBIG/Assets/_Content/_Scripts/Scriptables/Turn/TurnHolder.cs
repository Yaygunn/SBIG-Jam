using Managers.Turn;
using System;
using UnityEngine;

namespace Scriptables.Turn
{
    [CreateAssetMenu(fileName = "TurnHolder", menuName = "Scriptables/Turn/TurnHolder", order = -1)]
    public class TurnHolder : ScriptableObject
    {
        [SerializeField] private BaseTurn[] turns;

        private Action _endAction;
        private Action<BaseTurn> _playCoroutineAction;

        private int _turnIndex;
        public void Enter(Action endAction, Action<BaseTurn> playCoroutineAction)
        {
            _endAction = endAction;
            _playCoroutineAction = playCoroutineAction;
            _turnIndex = 0;
            TurnSwitch();
        }

        private void TurnSwitch()
        {
            if (_turnIndex >= turns.Length)
            {
                _endAction();
                return;
            }
            else
            {
                turns[_turnIndex].Enter(TurnSwitch);
                _playCoroutineAction(turns[_turnIndex]);
                _turnIndex++;
            }
        }
    }
}