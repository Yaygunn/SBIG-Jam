using System;
using System.Collections;
using System.Collections.Generic;
using Components.Weapons.Original;
using Managers.Global;
using TMPro;
using UnityEngine;

public class Temporary_PreviewUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _currentState;
    [SerializeField] private TextMeshProUGUI _currentMagazine;

    private void Start()
    {
        UpdateGameState("");
        UpdateMagazine();
    }

    private void OnEnable()
    {
        EventHub.Ev_ReloadFinished += UpdateMagazine;
    }

    private void OnDisable()
    {
        EventHub.Ev_ReloadFinished -= UpdateMagazine;
    }

    public void UpdateGameState(string state)
    {
        _currentState.text = state;
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
