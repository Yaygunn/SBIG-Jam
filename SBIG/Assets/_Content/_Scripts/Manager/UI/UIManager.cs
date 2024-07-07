using Controller.Player;
using Managers.Global;
using UnityEngine;
using Utilities.Singleton;

namespace Managers.UI
{
    public class UIManager : Singleton<UIManager>
    {
        [SerializeField] private GameObject _combatUI;

        [SerializeField] private GameObject _craftUI;
        
        [SerializeField] private Material _hdrSkybox;

        [SerializeField] private Color _hdrStartColor = new Color(0.3294118f, 0.3882353f, 0.6745098f);
        [SerializeField] private Color _hdrEndColor = new Color(0.3411765f, 0.07058824f, 0.08235294f);
        
        private PlayerController playerController;
        
        private void Start()
        {
            _hdrSkybox.SetColor("_Tint", _hdrStartColor);
            playerController = GlobalObject.Player.GetComponent<PlayerController>();
            EventHub.Ev_PlayerHealthChange += OnPlayerHealthChange;
        
            OnPlayerHealthChange();
        }
        
        private void OnDestroy()
        {
            _hdrSkybox.SetColor("_Tint", _hdrStartColor);
            EventHub.Ev_PlayerHealthChange -= OnPlayerHealthChange;
        }

        
        private void OnPlayerHealthChange()
        {
            int playerHealth = playerController.PlayerHealth;
            float t = (float) playerHealth / 100f;
            _hdrSkybox.SetColor("_Tint", Color.Lerp(_hdrEndColor, _hdrStartColor, t));
            // Start color #5363AC
            // End color #571215
        }


        public void ShowCombatUI()
        {
            _combatUI.SetActive(true);
            _craftUI.SetActive(false);
        }
    
        public void HideCombatUI()
        {
            _combatUI.SetActive(false);
        }

        public void ShowCraftUI()
        {
            _craftUI.SetActive(true);
        }

        public void HideCraftUI()
        {
            _craftUI.SetActive(false);
        }
    }
}
