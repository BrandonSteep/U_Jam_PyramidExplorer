using UnityEngine;

namespace HouseTrap.BadThoughts {
    public class ResistanceGuardStatus : AIStatus {
        [SerializeField] private Animator anim;
        [SerializeField] private Component[] componentsToDestroyOnDeath;

        protected override void Die() {
            anim.SetTrigger("Die");
            isAlive = false;
            foreach (var t in componentsToDestroyOnDeath) {
                Destroy(t);
            }
        }
    }
}