using Managers.LevelStart;
using System;
using UnityEngine;

public static class EventHub 
{
    #region Player
    public static event Action Ev_PlayerHealthChange;
    public static void PlayerHealthChange()
    {
        Ev_PlayerHealthChange?.Invoke();
    }
    
    public static event Action Ev_PlayerDied;
    public static void PlayerDied()
    {
        Ev_PlayerDied?.Invoke();
    }

    public static event Action Ev_PlayerCollision;

    public static void PlayerCollision()
    {
        Ev_PlayerCollision?.Invoke();
    }
    #endregion
    
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

    public static event Action Ev_CauldronStartCook;
    public static void CauldronStartCook()
    {
        Ev_CauldronStartCook?.Invoke();
    }

    public static event Action Ev_CauldronEndCook;
    public static void CauldronEndCook()
    {
        Ev_CauldronEndCook?.Invoke();
    }

    public static event Action Ev_CookFail;
    public static void CookFail()
    {
        Ev_CookFail?.Invoke();
    }
    
    public static event Action Ev_CauldronSpawned;
    public static void CauldronSpawned()
    {
        Ev_CauldronSpawned?.Invoke();
    }

    public static event Action Ev_ThrowInToCauldron;
    public static void ThrowInToCauldron()
    {
        Ev_ThrowInToCauldron?.Invoke();
    }

    public static event Action Ev_CropPicked;
    public static void CropPicked()
    {
        Ev_CropPicked?.Invoke();
    }

    #endregion

    #region Turn
    public static event Action Ev_TurnChange;
    public static void TurnChange()
    {
        Ev_TurnChange?.Invoke();
    }

    public static event Action Ev_CombatStart;
    public static void CombatStart()
    {
        Ev_CombatStart?.Invoke();
    }

    public static event Action Ev_CraftStart;
    public static void CraftStart()
    {
        Ev_CraftStart?.Invoke();
    }
    #endregion

    #region Weapon

    public static event Action Ev_SlapHit;
    public static void SlapHit()
    {
        Ev_SlapHit?.Invoke();
    }

    public static event Action Ev_Fired;
    public static void Fired()
    {
        Ev_Fired?.Invoke();
    }

    public static event Action Ev_ReloadStarted;
    public static void ReloadStarted()
    {
        Ev_ReloadStarted?.Invoke();
    }

    public static event Action Ev_ReloadFinished;
    public static void ReloadFinished()
    {
        Ev_ReloadFinished?.Invoke();
    }

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

    public static event Action<bool> Ev_ShowWeapon;
    public static void ShowWeapon(bool show)
    {
        Ev_ShowWeapon?.Invoke(show);
    }

    public static event Action Ev_Slapped;
    public static void Slapped()
    {
        Ev_Slapped?.Invoke();
    }

    public static event Action Ev_NoAmmo;
    public static void NoAmmo()
    {
        Ev_NoAmmo?.Invoke();
    }

    public static event Action Ev_Victory;
    public static void Victory()
    {
        Ev_Victory?.Invoke();
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

    #region Plants

    public static event Action Ev_GrowPlants;
    public static void GrowPlant()
    {
        Ev_GrowPlants?.Invoke();
    }

    #endregion

    #region Audio

    public static event Action<EStartMusic> Ev_StartMusic;
    public static void StartMusic(EStartMusic musicType)
    {
        Ev_StartMusic?.Invoke(musicType);
    }
    
    public static event Action Ev_ReduceMusicVolume;
    public static void ReduceMusicVolume()
    {
        Ev_ReduceMusicVolume?.Invoke();
    }
    
    public static event Action Ev_NormalizeMusicVolume;
    public static void NormalizeMusicVolume()
    {
        Ev_NormalizeMusicVolume?.Invoke();
    }

    #endregion

    #region Enemy
    public static event Action Ev_EnemyEndAndFlee;
    public static void EnemyEndAndFlee()
    {
        Ev_EnemyEndAndFlee?.Invoke();
    }
    
    public static event Action Ev_EnemyEndAndKill;
    public static void EnemyEndAndKill()
    {
        Ev_EnemyEndAndKill?.Invoke();
    }
    #endregion
    
    #region Prologue
    public static event Action Ev_ShowPrologueText;
    public static void ShowPrologueText()
    {
        Ev_ShowPrologueText?.Invoke();
    }
    
    public static event Action Ev_ClosePrologue;
    public static void ClosePrologue()
    {
        Ev_ClosePrologue?.Invoke();
    }

    public static event Action<bool> Ev_PauseGame;
    public static void PauseGame(bool pause)
    {
        Ev_PauseGame?.Invoke(pause);
    }
    
    #endregion
}
