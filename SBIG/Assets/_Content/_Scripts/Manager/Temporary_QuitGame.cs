using Controller.Player;
using Managers.Global;
using UnityEngine;

public class Temporary_QuitGame : MonoBehaviour
{
    private PlayerController playerController;
    private Temporary_PreviewUI _previewUI;
    [SerializeField] private Material _hdrSkybox;

    private void Start()
    {
        playerController = GlobalObject.Player.GetComponent<PlayerController>();
        _previewUI = FindObjectOfType<Temporary_PreviewUI>();
        
        EventHub.Ev_PlayerHealthChange += OnPlayerHealthChange;
        
        OnPlayerHealthChange();
    }
    
    private void OnDestroy()
    {
        EventHub.Ev_PlayerHealthChange -= OnPlayerHealthChange;
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

    private void OnPlayerHealthChange()
    {
        int playerHealth = playerController.PlayerHealth;
        float t = (float) playerHealth / 100f;
        _hdrSkybox.SetColor("_Tint", Color.Lerp(new Color(0.3411765f, 0.07058824f, 0.08235294f), new Color(0.3294118f, 0.3882353f, 0.6745098f), t));
        
        // Start color #5363AC
        // End color #571215
        
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
