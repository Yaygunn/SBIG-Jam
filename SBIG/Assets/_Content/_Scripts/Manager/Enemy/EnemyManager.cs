using System.Collections;
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
        
        #region Wave Spawner
        [Tooltip("How often to spawn an enemy")]
        public float WaveSpawnInterval = 5.0f;
        [Tooltip("How many enemies we're allowed on screen (starting)")]
        public int WaveEnemiesOnScreen = 5;
        [Tooltip("When the wave increases, how many extra enemies do we spawn")]
        public int IncreaseEnemiesPerWave = 1;
        [Tooltip("How often to increase the wave")]
        public float WaveIncreaseInterval = 20.0f;
        [Tooltip("The maximum amount of enemies allowed on screen (after wave increases)")]
        public int MaximumEnemiesOnScreen = 20;
        private int _initialWaveEnemiesOnScreen;
        private float _timeSinceLastWaveIncrease;
        private float _timeEnemiesStartedSpawning;
        #endregion
        
        public GameObject EnemyPrefab;
        public GameObject MiniEnemyPrefab;
        public List<Transform> SpawnLocations = new List<Transform>();
        
        public List<EnemyDataPair> EnemyDataList = new List<EnemyDataPair>();
        private Dictionary<EGolemType, EnemyData> _enemyDataDictionary;
        
        private Coroutine _enemyWaveCoroutine;
        
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

        private void Start()
        {
            ReleaseTheGolems();
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

        public void ReleaseTheGolems()
        {
            _initialWaveEnemiesOnScreen = WaveEnemiesOnScreen;
            _timeEnemiesStartedSpawning = Time.time;
            _enemyWaveCoroutine = StartCoroutine(EnemyWaveSpawner());
        }
        
        private IEnumerator EnemyWaveSpawner()
        {
            while (EnemyController.EnemyCount < WaveEnemiesOnScreen && EnemyController.EnemyCount < MaximumEnemiesOnScreen)
            {
                if (_timeEnemiesStartedSpawning + WaveIncreaseInterval < Time.time)
                {
                    WaveEnemiesOnScreen += IncreaseEnemiesPerWave;
                    _timeEnemiesStartedSpawning = Time.time;
                }
                
                SpawnEnemy();

                yield return new WaitForSeconds(WaveSpawnInterval);   
            }
        }

        public void EndAndFlee()
        {
            StopCoroutine(_enemyWaveCoroutine);
            
            EventHub.EnemyEndAndFlee();
        }

        public void EndAndKill()
        {
            StopCoroutine(_enemyWaveCoroutine);
            
            EventHub.EnemyEndAndKill();
        }
    }
    
    [System.Serializable]
    public struct EnemyDataPair
    {
        public EGolemType GolemType;
        public EnemyData Data;
    }
}