namespace HouseTrap.BadThoughts {
    public class State_AttackMelee : State {
        public override void RunState(AIStateMachineManager sm) {
            sm.FacePlayer();

            sm.SetDestination(sm.transform.position);
            sm.MeleeAttack();
        }
    }
}