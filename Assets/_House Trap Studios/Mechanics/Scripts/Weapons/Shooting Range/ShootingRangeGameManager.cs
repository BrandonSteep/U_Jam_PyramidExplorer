using HouseTrap.Core.EventSystem;
using HouseTrap.Core.ScriptableVariables;
using UnityEngine;

public class ShootingRangeGameManager : MonoBehaviour
{
    [SerializeField] private float maxTime;
    [SerializeField] private ScriptableVariableFloat timer;
    [SerializeField] private GameEvent gameOver;

    void OnEnable(){
        timer.value = maxTime;
    }

    void Update(){
        RunTimer();
    }

    private void RunTimer(){
        if(timer.value > 0){
            timer.value -= Time.deltaTime;
        }
        else if(timer.value <= 0){
            gameOver.Raise();
            Destroy(this.gameObject);
        }
    }
}
