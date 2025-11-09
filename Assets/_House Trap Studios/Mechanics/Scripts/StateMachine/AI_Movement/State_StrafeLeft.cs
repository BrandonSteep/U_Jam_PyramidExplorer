using UnityEngine;

namespace HouseTrap.BadThoughts {
    public class State_StrafeLeft : State {
        public override void RunState(AIStateMachineManager sm) {
            var offsetPlayer = sm.transform.position - sm.GetTarget().transform.position;
            var dir = Vector3.Cross(offsetPlayer, Vector3.up);
            sm.SetDestination(sm.transform.position + dir);

            // Turn to face the Player
            sm.FacePlayer();
        }
    }
}