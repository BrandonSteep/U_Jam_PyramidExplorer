using HouseTrap.Core.ScriptableVariables;
using TMPro;
using UnityEngine;

public class AmmoCounterUI : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private ScriptableVariableFloat count;
    
    void Update(){
        text.text = count.value.ToString("00");
    }
}
