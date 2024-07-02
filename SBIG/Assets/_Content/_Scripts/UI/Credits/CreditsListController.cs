using System.Collections.Generic;
using System.Linq;
using Scriptables.Credits;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Credits
{
    public class CreditsListController
    {
        private VisualTreeAsset _creditTemplate;
        private List<CreditData> _credits;
        private ListView _creditsList;
        
        public void Initialize(VisualElement root, VisualTreeAsset listElementTemplate)
        {
            LoadCredits();
            
            _creditTemplate = listElementTemplate;

            RenderCredits();
        }

        private void LoadCredits()
        {
            // Create a list and load all the credits under the resources folder\credits
            _credits = new List<CreditData>();
            _credits.AddRange(Resources.LoadAll<CreditData>("Credits"));
        }

        private void RenderCredits()
        {
            _creditsList.makeItem = () =>
            {
                var newListEntry = _creditTemplate.Instantiate();
                var newListEntryLogic = new CreditsListEntryController();
                newListEntry.userData = newListEntryLogic;
                newListEntryLogic.SetVisualElement(newListEntry);
                
                return newListEntry;
            };
            
            _creditsList.bindItem = (item, index) =>
            {
                (item.userData as CreditsListEntryController)?.SetCreditData(_credits[index]);
            };
        }
    }
}
