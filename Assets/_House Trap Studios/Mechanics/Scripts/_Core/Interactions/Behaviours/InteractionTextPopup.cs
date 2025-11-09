using UnityEngine;

namespace HouseTrap.Core.Interactions {
    public class InteractionTextPopup : MonoBehaviour, IInteractable {
        [SerializeField] private GameObject textBoxPrefab;
        [SerializeField] private string popupText;

        // [SerializeField] private bool typewriterEffect = true;
        [SerializeField] private float typeSpeed = 15f;

        [SerializeField] private bool isStatic;

        public virtual void Interact() {
            if (!isStatic) {
                TextBoxManager.CreateNewHoveringTextBox_TypewriterFX(popupText, textBoxPrefab, this.transform,
                    typeSpeed);
            } else {
                TextBoxManager.CreateNewStaticTextBox_TypewriterFX(popupText, textBoxPrefab, typeSpeed);
            }
        }
    }
}