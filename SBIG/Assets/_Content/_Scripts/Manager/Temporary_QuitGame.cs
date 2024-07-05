using System.Collections;
using System.Collections.Generic;
using Controller.Enemy;
using Controller.Player;
using Managers.Global;
using Scriptables.Enemy;
using UnityEngine;

public class Temporary_QuitGame : MonoBehaviour
{
    [SerializeField] private GameObject _golemPrefab;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private List<EnemyData> _enemyDataList;
    
    private PlayerController playerController;

    private void Start()
    {
        playerController = GlobalObject.Player.GetComponent<PlayerController>();
    }

    void Update()
    {
        QuitGame();

        SpawnGolem();

        ChangeGameState();
    }

    private void ChangeGameState()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (playerController.StateCurrent == playerController.StateCombat)
            {
                playerController.ChangeState(playerController.StateCraft);
            }
            else
            {
                playerController.ChangeState(playerController.StateCombat);
            }
        }
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
            // Do nothing, we might want to access the editor
#else
        Application.Quit();
#endif
        }
    }
}
