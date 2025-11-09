using UnityEngine;

namespace HouseTrap.BadThoughts {
    [CreateAssetMenu(menuName = "AI/Stats")]
    public class AIStats : ScriptableObject {
        [SerializeField] private float maxHealth;

        [SerializeField] private float movementIntervalMin = 1f;
        [SerializeField] private float movementIntervalMax = 3f;
        [SerializeField] private float attackIntervalMin = 0.5f;
        [SerializeField] private float attackIntervalMax = 3f;
        [SerializeField] private float meleeDistance = 2.5f;

        [SerializeField] private float projectileDamageMin = 5f;
        [SerializeField] private float projectileDamageMax = 25f;
        [SerializeField] private float projectileSpeed;
        [SerializeField] private float projectileLifetime = 10f;

        [SerializeField] private WeaponStats weaponStats;

        // Flags //
        [SerializeField] private bool isHitscan;

        #region Return Functions

        public float GetMaxHealth() {
            return maxHealth;
        }

        public Vector2 GetMovementInterval() {
            return new Vector2(movementIntervalMin, movementIntervalMax);
        }

        public Vector2 GetAttackInterval() {
            return new Vector2(attackIntervalMin, attackIntervalMax);
        }

        public float GetMeleeDistance() {
            return meleeDistance;
        }

        public float GetProjectileDamage() {
            return UnityEngine.Random.Range(projectileDamageMin, projectileDamageMax);
        }

        public float GetProjectileSpeed() {
            return projectileSpeed;
        }

        public float GetProjectileLifetime() {
            return projectileLifetime;
        }

        public bool IsHitscan() {
            return isHitscan;
        }

        #endregion
    }
}