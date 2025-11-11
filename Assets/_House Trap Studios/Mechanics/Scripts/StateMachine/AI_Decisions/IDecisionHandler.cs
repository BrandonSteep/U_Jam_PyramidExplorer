namespace HouseTrap.BadThoughts {
    public interface IDecisionHandler {
        public State MakeAIDecisions(AIStateMachineManager _sm);
        public void Stagger(AIStateMachineManager _sm);
    }
}