using UnityEngine;

namespace HouseTrap.Core.GameManagement {
    public class SettingsManager : MonoBehaviour {
        public static SettingsHolder SettingsHolder;
        public static PlayerSettingsSO PlayerSettings;
        public static ControllerSettingsSO ControllerSettings;
        public static InteractableSettingsSO InteractableSettings;

        private void Awake() {
            SettingsHolder = GetComponent<SettingsHolder>();
            PlayerSettings = SettingsHolder.GetPlayerSettings();
            ControllerSettings = SettingsHolder.GetControllerSettings();
            InteractableSettings = SettingsHolder.GetInteractableSettings();
            // Debug.Log($"{settingsHolder}, {playerSettings}, {controllerSettings}");
        }
    }
}