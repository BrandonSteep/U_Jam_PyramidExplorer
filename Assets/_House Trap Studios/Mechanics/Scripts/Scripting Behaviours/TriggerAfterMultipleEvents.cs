using HouseTrap.Core.EventSystem;
using UnityEngine;

public class TriggerAfterMultipleEvents : MonoBehaviour {
    [SerializeField] private int requiredTicks;
    private int currentTicks;

    [SerializeField] private GameEvent eventToTrigger;

    public void IncrementTicks() {
        currentTicks++;
        if (currentTicks >= requiredTicks) {
            TriggerEvent();
        }
    }

    private void TriggerEvent() {
        eventToTrigger.Raise();
    }
}
