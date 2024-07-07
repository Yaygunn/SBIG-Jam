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
        public GameObject MiniEnemyPrefab;
        public List<Transform> SpawnLocations = new List<Transform>();
        
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

        /**
         * Spawns a Random Golem at a random spawn location
         */
        public EnemyController SpawnEnemy()
        {
            if (SpawnLocations.Count == 0)
                return null;
            
            return SpawnEnemy(GetRandomGolemType(), SpawnLocations[Random.Range(0, SpawnLocations.Count)].position);
        }

        /**
         * Spawns a specific Golem Type in a random spawn location 
         */
        public EnemyController SpawnEnemy(EGolemType golemType)
        {
            if (SpawnLocations.Count == 0)
                return null;
            
            return SpawnEnemy(golemType, SpawnLocations[Random.Range(0, SpawnLocations.Count)].position);
        }
        
        /**
         * Spawns a specific Golem Type in the position provided
         */
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
        
        public void SpawnMiniEnemy(EGolemType golemType, Vector3 spawnPoint)
        {
            GameObject enemyGO = Instantiate(MiniEnemyPrefab, spawnPoint, Quaternion.identity);
            enemyGO.GetComponent<EnemyRagdoll>().Initialize( golemType );
        }
        
        private EGolemType GetRandomGolemType()
        {
            // Feels overkill but I'm not sure how else to pick a random one from an enum
            EGolemType[] values = (EGolemType[]) System.Enum.GetValues(typeof(EGolemType));
            int randomIndex = Random.Range(0, values.Length);
            
            return values[randomIndex];
        }
    }
    
    [System.Serializable]
    public struct EnemyDataPair
    {
        public EGolemType GolemType;
        public EnemyData Data;
    }
}