using UnityEngine;
using UnityEngine.UI;

namespace HouseTrap.BadThoughts {
    public class AbilityUIManager : MonoBehaviour {
        [SerializeField] private CurrentlyActiveAbility activeAbility;
        [SerializeField] private Image abilitySprite;

        [SerializeField] private SliderController cooldownSlider;

        public void SwapSpriteImage() {
            Debug.Log("Swapping UI Elements");

            abilitySprite.sprite = activeAbility.activeAbility.spriteImage;
            cooldownSlider.max = activeAbility.activeAbility.cooldownTimer;
        }
    }
}