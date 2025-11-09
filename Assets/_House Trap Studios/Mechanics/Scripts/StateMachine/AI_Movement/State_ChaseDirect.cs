using UnityEngine.AI;

namespace HouseTrap.BadThoughts {
    public class State_ChaseDirect : State {
        public override void OnStateEntered(AIStateMachineManager sm) {
            // If the Target isn't reachable - Strafe Instead //
            NavMeshPath path = sm.CalculatePath(sm.GetTarget().transform.position);
            if (path.status != NavMeshPathStatus.PathComplete) {
                float rand = UnityEngine.Random.Range(0f, 1f);
                if (rand > 0.5f) {
                    // Debug.Log("Path not Complete - Strafing Left instead");
                    sm.TransitionState(new State_StrafeLeft());
                } else {
                    // Debug.Log("Path not Complete - Strafing Right instead");
                    sm.TransitionState(new State_StrafeRight());
                }
            }
        }

        public override void RunState(AIStateMachineManager sm) {
            sm.SetDestination(sm.GetTarget().transform.position);
        }
    }
}