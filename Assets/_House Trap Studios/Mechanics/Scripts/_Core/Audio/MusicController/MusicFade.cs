using System.Collections;
using HouseTrap.Core.EventSystem;
using UnityEngine;

namespace HouseTrap.Core.Audio {
    public class MusicFade : MonoBehaviour {
        private AudioSource musicSource;
        [SerializeField] private float shortFadeDuration = .05f;
        [SerializeField] private float longFadeDuration = 2.5f;
        [SerializeField] private GameEvent fadeInEvent;
        [SerializeField] private GameEvent fadeOutEvent;

        public void Awake() { musicSource = GetComponent<AudioSource>(); }

        public void FadeIn() { StartCoroutine(PerformFade(0f, 1f, fadeInEvent, longFadeDuration)); }

        public void FadeOut() { StartCoroutine(PerformFade(1f, 0f, fadeOutEvent, longFadeDuration)); }

        public void FadeOutQuick() { StartCoroutine(PerformFade(1f, 0f, fadeOutEvent, shortFadeDuration)); }

        private IEnumerator PerformFade(float oldValue, float newValue, GameEvent eventToCall, float duration) {
            for (float t = 0f; t < duration; t += Time.deltaTime) {
                musicSource.volume = Mathf.Lerp(oldValue, newValue, t / duration);
                yield return null;
            }

            if (eventToCall != null) {
                eventToCall.Raise();
            }

            musicSource.volume = newValue;
        }
    }
}