namespace HouseTrap.BadThoughts {
    public class AIStateMachineBadThoughtBall : AIStateMachineManager {
        void OnEnable() {
            decisionHandler = new DecisionHandlerBadThoughtBall();
        }
    }
}