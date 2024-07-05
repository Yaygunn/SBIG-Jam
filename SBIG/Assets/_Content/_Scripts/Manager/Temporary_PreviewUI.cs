using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Temporary_PreviewUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _currentState;
    [SerializeField] private TextMeshProUGUI _currentMagazine;

    private void Start()
    {
        UpdateGameState("");
    }

    public void UpdateGameState(string state)
    {
        _currentState.text = "Current State: " + state;
    }

    public void UpdateMagazine()
    {
        _currentMagazine.text = "Current Magazine: " + "Magazine";
    }
}
