namespace HouseTrap.BadThoughts {
    public class State_IdleAnim : State {
        public override void OnStateEntered(AIStateMachineManager _sm) {
            _sm.Idle();
        }

        public override void RunState(AIStateMachineManager _sm) {

        }
    }
}