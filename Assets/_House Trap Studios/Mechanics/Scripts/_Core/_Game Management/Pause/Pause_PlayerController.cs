using HouseTrap.Core.EventSystem;
using UnityEngine;

namespace HouseTrap.Core.GameManagement {
    public class PausePlayerController : MonoBehaviour {
        private bool playerPaused;

        [SerializeField] protected GameEvent pausePlayerEvent;
        [SerializeField] protected GameEvent resumePlayerEvent;

        public void PausePlayer() {
            if (playerPaused) return;
            pausePlayerEvent.Raise();
            playerPaused = true;
        }

        public void ResumePlayer() {
            if (!playerPaused) return;
            resumePlayerEvent.Raise();
            playerPaused = false;
        }
    }
}