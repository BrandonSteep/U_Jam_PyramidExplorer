using UnityEngine;

namespace HouseTrap.BadThoughts {
    public class StateStrafeRightRootMotion : State {
        public override void RunState(AIStateMachineManager _sm) {
            var offsetPlayer = _sm.GetTarget().transform.position - _sm.transform.position;
            var dir = Vector3.Cross(offsetPlayer, Vector3.up);
            _sm.SetDestination(_sm.transform.position + dir);
        }
    }
}