using UnityEngine;

namespace HouseTrap.Core.GameManagement {
    public class PauseTime : MonoBehaviour {
        private bool gamePaused;

        public void PauseGame() {
            if (!gamePaused) {
                Time.timeScale = 0f;
                gamePaused = true;
            } else {
                Time.timeScale = 1f;
                gamePaused = false;
            }
        }
    }
}