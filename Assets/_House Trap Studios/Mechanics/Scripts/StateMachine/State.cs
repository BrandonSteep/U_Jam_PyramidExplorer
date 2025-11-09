namespace HouseTrap.BadThoughts {
    public class State {
        public virtual void RunState(AIStateMachineManager _sm) {
            // Debug.Log("Running Empty State");
            _sm.SetDestination(_sm.transform.position);
        }

        public virtual void OnStateEntered(AIStateMachineManager _sm) {

        }
    }
}