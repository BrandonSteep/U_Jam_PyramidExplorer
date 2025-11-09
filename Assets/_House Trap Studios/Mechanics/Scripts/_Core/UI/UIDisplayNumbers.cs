using HouseTrap.Core.ScriptableVariables;
using TMPro;
using UnityEngine;

namespace HouseTrap.Core.UI {
    public class UIDisplayNumbers : MonoBehaviour {
        [SerializeField] private TMP_Text textBox;
        [SerializeField] private ScriptableVariableFloat floatToDisplay;
        [SerializeField] private UINumericalStringType displayType = UINumericalStringType.WholeNumbers;
        private float currentValue;

        private void Awake() {
            if (!textBox) {
                textBox = GetComponent<TMP_Text>();
            }

            currentValue = floatToDisplay.value;
        }

        private void Update() {
            if (Mathf.Approximately(currentValue, floatToDisplay.value)) return;
            currentValue = floatToDisplay.value;
            textBox.text = displayType switch {
                UINumericalStringType.WholeNumbers => currentValue.ToString("F0"),
                UINumericalStringType.TwoDecimalPlaces => currentValue.ToString("F"),
                _ => textBox.text
            };
        }
    }
}