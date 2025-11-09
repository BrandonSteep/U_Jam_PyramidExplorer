using HouseTrap.Core.ScriptableVariables;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour {
    [SerializeField] private float maxFloat;
    public ScriptableVariableFloat max;
    [SerializeField] private ScriptableVariableFloat current;
    private float sliderValue;

    [SerializeField] private Slider slider;

    [SerializeField] private Image image;
    [SerializeField] private Gradient gradient;

    private void Awake() { slider = GetComponent<Slider>(); }

    private void Update() {
        if (max) {
            sliderValue = current.value / max.value;
        } else {
            sliderValue = current.value / maxFloat;
        }

        slider.value = sliderValue;
        image.color = ColorFromGradient(sliderValue);
    }

    private Color ColorFromGradient(float _value) { return gradient.Evaluate(_value); }
}