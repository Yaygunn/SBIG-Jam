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

    public static event Action<string> Ev_ShowCookInCauldronText;
    public static void ShowCookInCauldronText(string text)
    {
        Ev_ShowCookInCauldronText?.Invoke(text);
    }

    #endregion

    #region Weapon

    public static event Action Ev_MagazineEnded;
    public static void MagazineEnded()
    {
        Ev_MagazineEnded?.Invoke();
    }

    public static event Action Ev_ForceEndMagazine;
    public static void ForceEndMagazine()
    {
        ForceEndMagazine();
    }
    #endregion

    #region Menu
    public static event Action Event_StartMenu;
    public static void StartMenu()
    {
        Event_StartMenu?.Invoke();
    }
    
    #endregion
    
    #region UI
    public static event Action Event_UIHover;
    public static void UIHover()
    {
        Event_UIHover?.Invoke();
    }
    public static event Action Event_UIClick;
    public static void UIClick()
    {
        Event_UIClick?.Invoke();
    }
    public static event Action Event_UISlider;
    public static void UISlider()
    {
        Event_UISlider?.Invoke();
    }
        
    #endregion
}
