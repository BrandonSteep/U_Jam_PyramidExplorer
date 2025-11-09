using UnityEngine;

namespace HouseTrap.Core.Audio {
    public class StopMusic : MonoBehaviour {
        [SerializeField] private float _fadeDuration;

        public void FadeMusicOut() { MusicManager.FadeOut(_fadeDuration); }
    }
}