using Scriptables.Credits;
using UnityEngine.UIElements;

namespace UI.Credits
{
    public class CreditsListEntryController
    {
        private Label _positionLabel;
        private Label _nameLabel;
        private VisualElement _flagImage;
        
        public void SetVisualElement(VisualElement visualElement)
        {
            _positionLabel = visualElement.Q<Label>("Position");
            _nameLabel = visualElement.Q<Label>("Name");
            _flagImage = visualElement.Q<Image>("Flag");
        }

        public void SetCreditData(CreditData data)
        {
            _positionLabel.text = data.Position;
            _nameLabel.text = data.Name;
            _flagImage.style.backgroundImage = data.Flag.texture;
        }
    }
}