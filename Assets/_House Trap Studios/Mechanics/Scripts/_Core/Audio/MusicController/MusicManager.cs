using System.Collections;
using UnityEngine;

namespace HouseTrap.Core.Audio {
    public class MusicManager : MonoBehaviour {
        public static MusicManager Instance;

        [SerializeField] private AudioClip currentTrack;
        [SerializeField] private AudioClip nextTrack;
        private static AudioSource aSource;

        [SerializeField] private float defaultFadeLength = 2f;
        private static float musicVolume = 1f;

        private void Awake() {
            if (Instance != null && Instance != this) {
                Debug.Log("Multiple Music Managers found - Destroying this one");
                Destroy(this.gameObject);
            } else {
                Instance = this;

                aSource = GetComponent<AudioSource>();
                FadeIn();
            }
        }

        public static void ChangeTrack(AudioClip newTrack, float fadeDuration) {
            Instance.nextTrack = newTrack;
            Instance.FadeIn(fadeDuration);
        }

        private void FadeIn() {
            aSource.volume = 0f;
            currentTrack = nextTrack;
            StartMusic();
            StartCoroutine(Fade(defaultFadeLength, musicVolume));
        }

        public void FadeOut() {
            StartCoroutine(Fade(defaultFadeLength, musicVolume));
            Invoke("StopMusic", defaultFadeLength);
        }

        private void FadeIn(float duration) {
            aSource.volume = 0f;
            currentTrack = nextTrack;
            StartMusic();
            StartCoroutine(Fade(duration, musicVolume));
        }

        public static void FadeOut(float duration) {
            Instance.StartCoroutine(Instance.Fade(duration, musicVolume));
            // Invoke("StopMusic", duration);
        }

        public IEnumerator Fade(float duration, float targetVolume) {
            float time = 0f;
            float startVol = aSource.volume;
            while (time < duration) {
                time += Time.deltaTime;
                aSource.volume = Mathf.Lerp(startVol, targetVolume, time / duration);
                yield return null;
            }
        }

        private void StartMusic() {
            Debug.Log("Starting Music");
            aSource.clip = currentTrack;
            aSource.Play();
        }

        private void StopMusic() {
            Debug.Log("Stopping Music");
            aSource.Stop();
        }
    }
}