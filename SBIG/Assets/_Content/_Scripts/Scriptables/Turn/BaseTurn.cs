using System;
using System.Collections;
using UnityEngine;

public class BaseTurn : ScriptableObject
{
    protected Action _endAction { get; private set; }
    public void Enter(Action endAction)
    {
        _endAction = endAction;
    }

    public virtual IEnumerator TurnOperations()
    {
        yield return null;
    }

    protected virtual void EndTurn()
    {
        _endAction();
    }
}
