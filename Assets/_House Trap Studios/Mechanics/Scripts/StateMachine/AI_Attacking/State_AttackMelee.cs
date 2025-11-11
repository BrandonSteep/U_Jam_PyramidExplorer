namespace HouseTrap.BadThoughts {
    public class State_AttackMelee : State {
        public override void RunState(AIStateMachineManager _sm) {
            _sm.FacePlayer();

            _sm.SetDestination(_sm.transform.position);
            _sm.MeleeAttack();
        }
    }
}