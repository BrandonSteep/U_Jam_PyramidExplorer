using UnityEngine;

namespace HouseTrap.Core.Audio {
    public class AutoMusicFade : MusicFade {
        [SerializeField] private float delayTime;

        [Tooltip("Overrides Fade Out On Awake, if selected")] [SerializeField]
        private bool fadeInOnAwake;

        [Tooltip("Overrides Fade Out On Awake, if selected")] [SerializeField]
        private bool fadeOutOnAwake;

        private void Start() {
            if (fadeInOnAwake) {
                Invoke(nameof(FadeIn), delayTime);
            } else if (fadeOutOnAwake) {
                Invoke(nameof(FadeOut), delayTime);
            }
        }
    }
}