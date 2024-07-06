using Controller.Player;
using Managers.Global;
using UnityEngine;

public class Temporary_QuitGame : MonoBehaviour
{
    private PlayerController playerController;
    private Temporary_PreviewUI _previewUI;

    private void Start()
    {
        playerController = GlobalObject.Player.GetComponent<PlayerController>();
        _previewUI = FindObjectOfType<Temporary_PreviewUI>();
    }

    void Update()
    {
        QuitGame();

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
