using System.Collections;
using System.Collections.Generic;
using Controller.Enemy;
using Scriptables.Enemy;
using UnityEngine;

public class Temporary_QuitGame : MonoBehaviour
{
    [SerializeField] private GameObject _golemPrefab;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private List<EnemyData> _enemyDataList;
    void Update()
    {
        QuitGame();

        SpawnGolem();
    }

    private void SpawnGolem()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            GameObject enemyGO = Instantiate(_golemPrefab, _spawnPoint.position, Quaternion.identity);
            EnemyController enemy = enemyGO.GetComponent<EnemyController>();

            if (enemy)
            {
                int randomIndex = Random.Range(0, _enemyDataList.Count);
                
                enemy.Initialize( _enemyDataList[randomIndex] );
            }
        }
    }

    private void QuitGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        }
    }
}
