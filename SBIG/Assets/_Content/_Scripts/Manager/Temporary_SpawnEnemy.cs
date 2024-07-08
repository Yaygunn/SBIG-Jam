using System;
using System.Collections;
using System.Collections.Generic;
using Controller.Enemy;
using Enums.Golem;
using Scriptables.Enemy;
using TMPro;
using UnityEngine;

public class Temporary_SpawnEnemy : MonoBehaviour
{
    public EGolemType GolemType;
    public GameObject EnemyPrefab;
    public EnemyData RockGolemData;
    public EnemyData FireGolemData;
    public EnemyData WaterGolemData;
    public EnemyData WoodGolemData;
    
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private TextMeshPro _golemText;
    private Collider _collider;
    
    private void Awake()
    {
        _collider = GetComponent<Collider>();

        SetGolemText();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SpawnEnemy();
        }
    }

    private void SetGolemText()
    {
        switch (GolemType)
        {
            case EGolemType.ROCK:
                _golemText.text = "Rock Golem";
                break;
            case EGolemType.FIRE:
                _golemText.text = "Fire Golem";
                break;
            case EGolemType.WATER:
                _golemText.text = "Water Golem";
                break;
            case EGolemType.WOOD:
                _golemText.text = "Wood Golem";
                break;
        }
    }

    private void SpawnEnemy()
    {
        GameObject enemy;
        
        switch (GolemType)
        {
            case EGolemType.ROCK:
                enemy = Instantiate(EnemyPrefab, _spawnPoint.position, Quaternion.identity);
                enemy.GetComponent<EnemyController>().Initialize(RockGolemData);
                break;
            case EGolemType.FIRE:
                enemy = Instantiate(EnemyPrefab, _spawnPoint.position, Quaternion.identity);
                enemy.GetComponent<EnemyController>().Initialize(FireGolemData);
                break;
            case EGolemType.WATER:
                enemy = Instantiate(EnemyPrefab, _spawnPoint.position, Quaternion.identity);
                enemy.GetComponent<EnemyController>().Initialize(WaterGolemData);
                break;
            case EGolemType.WOOD:
                enemy = Instantiate(EnemyPrefab, _spawnPoint.position, Quaternion.identity);
                enemy.GetComponent<EnemyController>().Initialize(WoodGolemData);
                break;
        }
    }
}
