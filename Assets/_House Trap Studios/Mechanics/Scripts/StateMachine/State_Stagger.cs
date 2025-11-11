using UnityEngine;

namespace HouseTrap.BadThoughts {
    public class State_Stagger : State {
        public override void RunState(AIStateMachineManager _sm) {
            Debug.Log("ENEMY STAGGERED!!!");
        }
    }
}