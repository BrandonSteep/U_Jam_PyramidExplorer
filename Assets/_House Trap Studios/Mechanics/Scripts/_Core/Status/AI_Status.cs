using UnityEngine;

namespace HouseTrap.BadThoughts {
    public class AIStatus : Status, IDamageable {
        [SerializeField] private AIStats stats;
        [SerializeField] private float currentHealth;
        [SerializeField] private AIStateMachineManager sm;

        private void Awake() {
            currentHealth = stats.GetMaxHealth();
            if (!sm) {
                sm = GetComponent<AIStateMachineManager>();
            }
        }

        public void TakeDamage(RaycastHit _hit, float _damageAmount, GameObject _attackOrigin) {
            if (!canTakeDamage || !isAlive) return;
            if (!sm.AwareOfTarget()) {
                sm.TargetFound();
            }

            // Debug.Log($"AI took {_damageAmount} points of damage from {_attackOrigin}");
            currentHealth -= _damageAmount;

            PlayHitSound();

            if (currentHealth <= 0f) {
                Die();
            }
        }

        public void TakeDamage(Transform _other, float _damageAmount, float _forceAmount) {
            if (!canTakeDamage || !isAlive) return;
            if (!sm.AwareOfTarget()) {
                sm.TargetFound();
            }
            
            sm.Stagger(_other, _forceAmount);

            Debug.Log($"AI took {_damageAmount} points of damage from {_other.gameObject.name}");
            currentHealth -= _damageAmount;

            PlayHitSound();

            if (currentHealth <= 0f) {
                Die();
            }
            // else{
            //     AddIFrames(iFramesInSeconds);
            // }
        }
    }
}