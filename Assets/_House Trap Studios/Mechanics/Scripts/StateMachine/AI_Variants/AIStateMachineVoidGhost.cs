using UnityEngine;

namespace HouseTrap.BadThoughts {
    public class AIStateMachineVoidGhost : AIStateMachineManager {
        [SerializeField] private Animator anim;

        public override void RangedAttack() {
            anim.SetTrigger("Fire");
        }
    }
}