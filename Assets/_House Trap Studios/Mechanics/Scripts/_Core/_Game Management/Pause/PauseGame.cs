using HouseTrap.Core.Controller;
using UnityEngine;

namespace HouseTrap.Core.GameManagement {
    public class PauseGame : MonoBehaviour {
        [SerializeField] private GameObject pauseMenu;

        public void Pause() {
            PlayerController.UnlockCursor();
            ControllerReferences.PlayerController.DisablePlayerController();

            Time.timeScale = 0f;
            pauseMenu.SetActive(true);
        }

        public void Resume() {
            SettingsMenu.ApplySettings();
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
            PlayerController.LockCursor();
            ControllerReferences.PlayerController.EnablePlayerController();
        }
    }
}