namespace HouseTrap.BadThoughts {
    public class AIStateMachineBadThoughtBall : AIStateMachineManager {
        void OnEnable() {
            DecisionHandler = new DecisionHandlerBadThoughtBall();
        }
    }
}