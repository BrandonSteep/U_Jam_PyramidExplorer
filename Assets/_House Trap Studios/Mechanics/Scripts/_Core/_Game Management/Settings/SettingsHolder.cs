using HouseTrap.Core.GameManagement;
using UnityEngine;

public class SettingsHolder : MonoBehaviour {
    [SerializeField] private PlayerSettingsSO playerSettings;
    [SerializeField] private ControllerSettingsSO controllerSettings;
    [SerializeField] private InteractableSettingsSO interactableSettings;

    public PlayerSettingsSO GetPlayerSettings() { return playerSettings; }
    public ControllerSettingsSO GetControllerSettings() { return controllerSettings; }
    public InteractableSettingsSO GetInteractableSettings(){ return interactableSettings; }
}
