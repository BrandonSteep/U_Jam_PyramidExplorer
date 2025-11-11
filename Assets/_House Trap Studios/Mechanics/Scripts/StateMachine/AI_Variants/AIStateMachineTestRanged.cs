namespace HouseTrap.BadThoughts {
    public class AIStateMachineTestRanged : AIStateMachineManager {
        private void Awake() {
            decisionHandler = new DecisionHandlerRanged();
        }
    }
}