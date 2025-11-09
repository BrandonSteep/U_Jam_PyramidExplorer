using UnityEngine;

namespace HouseTrap.Core.Audio {
    public class SoundArray : MonoBehaviour {
        [SerializeField] private bool canPlay = true;
        [SerializeField] private AudioClip[] sounds;

        private int soundsLength;
        private AudioSource source;

        [SerializeField] private float minPitch = 0.95f;
        [SerializeField] private float maxPitch = 1.05f;


        private void OnEnable() {
            source = GetComponent<AudioSource>();
            if (source == null) {
                source = this.gameObject.AddComponent<AudioSource>();
            }

            soundsLength = sounds.Length;
        }

        public void PlaySound(int _sound) {
            if (!canPlay) return;
            source.pitch = UnityEngine.Random.Range(minPitch, maxPitch);
            source.PlayOneShot(sounds[_sound]);
        }

        public virtual void PlayRandom() {
            if (!canPlay) return;
            source.pitch = UnityEngine.Random.Range(minPitch, maxPitch);
            source.PlayOneShot(sounds[UnityEngine.Random.Range(0, soundsLength)]);
        }

        public void EnableSoundArray(bool _active) {
            canPlay = _active;
        }
    }
}