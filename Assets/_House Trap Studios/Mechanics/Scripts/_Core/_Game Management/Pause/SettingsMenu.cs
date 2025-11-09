using HouseTrap.Core.Controller;
using HouseTrap.Core.GameManagement;
using UnityEngine;
using UnityEngine.UI;

namespace HouseTrap.Core {
    public class SettingsMenu : MonoBehaviour {
        // public static SettingsMenu Instance;
        private static PlayerSettingsSO playerSettingsSo;

        [SerializeField] private Slider fovSlider;
        [SerializeField] private Slider sensitivitySlider;
        [SerializeField] private Slider mouseSmoothingSlider;
        private static Slider fovSliderStatic;
        private static Slider sensitivitySliderStatic;
        private static Slider mouseSmoothingSliderStatic;
        private static bool menuOpen;

        private void Awake() {
            // Instance = this;
            playerSettingsSo = SettingsManager.PlayerSettings;
            // Debug.Log($"Player Settings = {playerSettingsSO}");

            fovSliderStatic = fovSlider;
            sensitivitySliderStatic = sensitivitySlider;
            mouseSmoothingSliderStatic = mouseSmoothingSlider;
        }

        private static void SetMenu() {
            fovSliderStatic.value = playerSettingsSo.GetCameraFov();
            // Will need to swap out for two sliders for X & Y
            sensitivitySliderStatic.value = playerSettingsSo.GetMouseSensitivity().x;
            mouseSmoothingSliderStatic.value = playerSettingsSo.GetMouseSmoothTime();

            OpenMenu();
        }

        public static void ApplySettings() {
            if (!menuOpen) return;
            playerSettingsSo.UpdateSettings(fovSliderStatic.value,
                new Vector2(sensitivitySliderStatic.value, sensitivitySliderStatic.value),
                mouseSmoothingSliderStatic.value);
            ControllerReferences.PlayerController.UpdatePlayerSettings();
        }

        private void OnEnable() { SetMenu(); }
        private void OnDisable() { CloseMenu(); }
        private static void OpenMenu() { menuOpen = true; }
        private static void CloseMenu() { menuOpen = false; }
    }
}