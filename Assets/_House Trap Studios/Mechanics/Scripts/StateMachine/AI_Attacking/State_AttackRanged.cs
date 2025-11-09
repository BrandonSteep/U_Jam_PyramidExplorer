namespace HouseTrap.BadThoughts {
    public class State_AttackRanged : State {
        public override void RunState(AIStateMachineManager sm) {
            sm.FacePlayer();

            sm.SetDestination(sm.transform.position);
            sm.RangedAttack();
        }
    }
}