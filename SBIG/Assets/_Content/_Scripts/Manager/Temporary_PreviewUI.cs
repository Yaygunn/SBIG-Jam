using System;
using System.Collections;
using System.Collections.Generic;
using Components.Weapons.Original;
using Controller.Player;
using Managers.Global;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.LowLevel;
using Random = UnityEngine.Random;

public class Temporary_PreviewUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _currentState;
    [SerializeField] private TextMeshProUGUI _currentMagazine;

    private void Start()
    {
        UpdateGameState();
        UpdateMagazine();
    }

    private void OnEnable()
    {
        EventHub.Ev_ReloadFinished += UpdateMagazine;
        EventHub.Ev_TurnChange += UpdateGameState;
    }

    private void OnDisable()
    {
        EventHub.Ev_ReloadFinished -= UpdateMagazine;
        EventHub.Ev_TurnChange -= UpdateGameState;
    }

    public void UpdateGameState()
    {
        PlayerController player = GlobalObject.Player.GetComponent<PlayerController>();
        String stateText = (player.StateCurrent == player.StateCombat) ? "Combat" : "Craft";
        
        _currentState.text = stateText;
    }

    public void UpdateMagazine()
    {
        var magazine = GlobalObject.Player.GetComponentInChildren<Weapon>().CurrentMagazine;
        
        if (magazine == null)
        {
            _currentMagazine.text = "Empty";
            return;
        }
        
        _currentMagazine.text = magazine.name;
    }
}
