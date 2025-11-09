using HouseTrap.Core.ScriptableVariables;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] private ScriptableVariableFloat score;
    [SerializeField] private ScriptableVariableFloat highScore;

    public void IncreaseScoreBy10(){
        score.value += 10;
        UpdateHighScore();
    }

    public void IncreaseScoreBy25(){
        score.value += 25;
        UpdateHighScore();
    }

    private void UpdateHighScore(){
        if(score.value > highScore.value){
            highScore.value = score.value;
        }
    }
}