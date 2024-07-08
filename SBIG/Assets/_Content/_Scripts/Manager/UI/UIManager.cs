using Controller.Player;
using Managers.Global;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
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
        [SerializeField] private Image _vImage;
        
        private PlayerController playerController;
        
        private void Start()
        {
            _hdrSkybox.SetColor("_Tint", _hdrStartColor);
            playerController = GlobalObject.Player.GetComponent<PlayerController>();
            EventHub.Ev_PlayerHealthChange += OnPlayerHealthChange;
            EventHub.Ev_Victory += StartVictory;
            OnPlayerHealthChange();
        }
        
        private void OnDestroy()
        {
            _hdrSkybox.SetColor("_Tint", _hdrStartColor);
            EventHub.Ev_PlayerHealthChange -= OnPlayerHealthChange;
            EventHub.Ev_Victory -= StartVictory;
        }

        private void StartVictory()
        {
            StartCoroutine(victory());
        }

        private IEnumerator victory()
        {
            Color c = _vImage.color;
            float alphaChange = 2;
            float total = 0;
            while (true)
            {
                yield return null;
                total+= alphaChange*Time.deltaTime;
                if (total > 1)
                {
                    total = 1;
                    c.a = total;
                    _vImage.color = c;

                    break;
                }
                    c.a = total;
                _vImage.color = c;
            }
            yield return new WaitForSeconds(3);
            while (true)
            {
                yield return null;
                total -= alphaChange * Time.deltaTime;
                if (total < 0)
                {
                    total = 0;
                    c.a = total;
                    _vImage.color = c;

                    break;
                }
                c.a = total;
                _vImage.color = c;

            }
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
