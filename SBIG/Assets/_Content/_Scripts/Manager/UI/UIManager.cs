using UnityEngine;
using Utilities.Singleton;

namespace Managers.UI
{
    public class UIManager : Singleton<UIManager>
    {
        [SerializeField] private GameObject _combatUI;

        [SerializeField] private GameObject _craftUI;

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
    }
}
