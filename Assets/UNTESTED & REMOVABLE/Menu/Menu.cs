using HouseTrap.Core.EventSystem;
using HouseTrap.Core.ScriptableVariables;
using UnityEngine;
using UnityEngine.UI;

namespace HouseTrap.BadThoughts {
    public class Menu : MonoBehaviour {
        [SerializeField] private GameObject menu;
        [SerializeField] private ScriptableVariableFloat sensitivity;
        [SerializeField] private GameEvent sensChange;
        [SerializeField] private Slider slider;

        public void ToggleMenu() {
            Cursor.visible = !Cursor.visible;
            Cursor.lockState = CursorLockMode.Locked;
            menu.SetActive(!menu.activeInHierarchy);

            if (menu.activeInHierarchy) {
                Cursor.lockState = CursorLockMode.Confined;
            }
        }

        public void SetSensitivity() {
            sensitivity.value = slider.value;
            sensChange.Raise();
        }
    }
}