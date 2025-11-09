using UnityEngine;

namespace HouseTrap.Core.Audio {
    [CreateAssetMenu(menuName = "Scriptable Objects/Audio Clip Array")]
    public class AudioClipArray : ScriptableObject {
        public AudioClip[] audioClips;
    }
}