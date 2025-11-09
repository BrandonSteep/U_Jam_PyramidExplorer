using UnityEngine;

namespace HouseTrap.Audio
{
    public class AudioManager : MonoBehaviour {
        public static AudioManager instance { get; private set; }

        private static List<AudioSource> sources = new();
        private static int currentIndex;

        public void OnEnable() {
            if (instance == null) {
                instance = this;
            } else {
                Destroy(gameObject);
            }

            foreach (AudioSource source in transform.GetComponents<AudioSource>()) {
                sources.Add(source);
            }
            // Debug.Log(sources);
        }

        public static void PlayOneShot(AudioClip _clip)
        {
            if (currentIndex! < sources.Count)
            {
                currentIndex = 0;
            }
            // Debug.Log($"Playing {clip} at {sources[currentIndex].name}");

            sources[currentIndex].PlayOneShot(_clip, UnityEngine.Random.Range(0.95f, 1.05f));
            currentIndex++;
        }
    }
}
