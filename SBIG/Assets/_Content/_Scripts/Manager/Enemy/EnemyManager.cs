using System.Collections.Generic;
using Controller.Enemy;
using Enums.Golem;
using Scriptables.Enemy;
using UnityEngine;

namespace Manager.Enemy
{
    public class EnemyManager: MonoBehaviour
    {
        public static EnemyManager Instance { get; private set; }
        public GameObject EnemyPrefab;
        
        public List<EnemyDataPair> EnemyDataList = new List<EnemyDataPair>();
        private Dictionary<EGolemType, EnemyData> _enemyDataDictionary;
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                InitializeDictionary();
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
        private void InitializeDictionary()
        {
            _enemyDataDictionary = new Dictionary<EGolemType, EnemyData>();
            foreach (var pair in EnemyDataList)
            {
                _enemyDataDictionary[pair.GolemType] = pair.Data;
            }
        }
        
        public EnemyController SpawnEnemy(EGolemType golemType, Vector3 spawnPoint)
        {
            if (_enemyDataDictionary.ContainsKey(golemType))
            {
                GameObject enemyGO = Instantiate(EnemyPrefab, spawnPoint, Quaternion.identity);
                EnemyController enemy = enemyGO.GetComponent<EnemyController>();

                if (enemy)
                {
                    enemy.Initialize( _enemyDataDictionary[golemType] );
                    
                    return enemy;
                }
            }
            
            return null;
        }
    }
    
    [System.Serializable]
    public struct EnemyDataPair
    {
        public EGolemType GolemType;
        public EnemyData Data;
    }
}