using DG.Tweening;
using TMPro;
using UnityEngine;

namespace HouseTrap.Core.UI {
    public class UITextFade : MonoBehaviour {
        private Tween fadeTween;
        [SerializeField] private TMP_Text textBox;

        [SerializeField] private Color fadedColour;
        [SerializeField] private bool fadeOnEnable;
        [SerializeField] private float fadeTime = 2f;
        [SerializeField] private float delayTime = 5f;


        private void Awake() {
            if (fadeOnEnable) {
                FadeAfterDelay();
            }
        }

        public void FadeAfterDelay() { Invoke(nameof(Fade), delayTime); }

        public void Fade() {
            if (fadeTween != null) {
                // fadeTween.Kill(false);
            } else {
                fadeTween = textBox.DOColor(fadedColour, fadeTime);
                Destroy(this.gameObject, fadeTime);
            }
        }

        public bool GetFadeOnEnable() { return fadeOnEnable; }
    }
}