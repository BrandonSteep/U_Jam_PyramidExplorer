using HouseTrap.Core.ScriptableVariables;
using TMPro;
using UnityEngine;

public class TimerUI : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private ScriptableVariableFloat timer;

    public void StartTimer(){
        // Invoke("SetCount", 0.1f);
    }

    private void Update(){
        text.text = GetFormat();
    }

    private string GetFormat(){
        if(timer.value >= 0){
            string minutes = Mathf.Floor(timer.value / 60).ToString("00");
            string seconds = (timer.value % 60).ToString("00");

            return $"{minutes}:{seconds}";
        }
        else{
            return "00:00";
        }
    }
}
