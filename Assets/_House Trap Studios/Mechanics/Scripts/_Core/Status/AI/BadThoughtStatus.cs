using UnityEngine;

namespace HouseTrap.BadThoughts {
    public class BadThoughtStatus : AIStatus {
        [SerializeField] private GameObject deathExplosion;

        protected override void Die() {
            Instantiate(deathExplosion, this.transform.GetChild(0).transform.position, Quaternion.identity);
            base.Die();
        }
    }
}