using System;
using UnityEngine;

public static class EventHub 
{
    #region CarrySystem

    public static event Action Ev_CloseCarryTexts;
    public static void CloseCarryTexts()
    {
        Ev_CloseCarryTexts?.Invoke();
    }

    public static event Action<string, bool> Ev_ShowPickableText;
    public static void ShowPickableText(string text, bool isPickable)
    {
        Ev_ShowPickableText?.Invoke(text, isPickable);
    }

    public static event Action<string> Ev_ShowThrowToCauldronText;
    public static void ShowThrowToCauldronText(string text)
    {
        Ev_ShowThrowToCauldronText?.Invoke(text);
    }

    #endregion
}
