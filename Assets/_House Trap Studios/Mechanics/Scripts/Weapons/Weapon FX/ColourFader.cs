using HouseTrap.Core.ScriptableVariables;
using TMPro;
using UnityEngine;

public class ColourFader : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private ScriptableVariableFloat count;
    [SerializeField] private ScriptableVariableFloat maxCount;
    [SerializeField] private Gradient gradient;

    private void Update(){
        Invoke("SetColour", .1f);
    }

    private void SetColour(){
        float percentage = (count.value / maxCount.value);
        // Debug.Log(percentage);
        text.color = ColorFromGradient(percentage);
    }

        Color ColorFromGradient(float value)  // float between 0-1
    {
        return gradient.Evaluate(value);
    }
}
