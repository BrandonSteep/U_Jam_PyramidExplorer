using HouseTrap.Core.Controller;
using HouseTrap.Core.EventSystem;
using HouseTrap.Core.ScriptableVariables;
using UnityEngine;

namespace HouseTrap.Core {
    public class PlayerStatus : Status, IDamageable {
        [SerializeField] public ScriptableVariableFloat currentHp;
        [SerializeField] public ScriptableVariableFloat maxHp;

        [SerializeField] private ScriptableVariableFloat currentArmour;
        [SerializeField] private ScriptableVariableFloat maxArmour;

        [SerializeField] private float iFramesInSeconds;
        [SerializeField] private float knockbackForce;

        [Header("Events")] [SerializeField] private GameEvent playerHurtSmall;
        [SerializeField] private GameEvent playerHurtMedium;
        [SerializeField] private GameEvent playerHurtLarge;
        [SerializeField] private GameEvent playerHurtKilled;

        [SerializeField] private SceneControllerSO deathScreen;

        private void Awake() {
            currentHp.value = maxHp.value;
            currentArmour.value = maxArmour.value;
        }

        #region Damage Calculation

        public void TakeDamage(RaycastHit _hit, float _damageAmount, GameObject _attackOrigin) {
            Debug.LogWarning($"Player hit with projectile from {_attackOrigin}, dealing {_damageAmount} damage");
            if (!canTakeDamage || !isAlive) return;
            var damageToDeal = DamageWithArmourAdjustments(_damageAmount);
            Debug.Log($"Taking {damageToDeal} points of damage");
            currentHp.value -= damageToDeal;

            switch (damageToDeal) {
                case <= 15f: playerHurtSmall.Raise();
                    break;
                case > 15f and <= 50f: playerHurtMedium.Raise();
                    break;
                case > 50f when currentHp.value > 0f: playerHurtLarge.Raise();
                    break;
            }

            ControllerReferences.knockback.AddImpact(transform.position - _attackOrigin.transform.position,
                knockbackForce, ForceMode.Impulse);
            if (currentHp.value <= 0f) {
                Die();
            } else if (iFramesInSeconds > 0f) {
                AddIFrames(iFramesInSeconds);
            }
        }

        public void TakeDamage(Transform _other, float _damageAmount) {
            // Debug.Log($"Player hit with projectile from {_other.gameObject.name}, dealing {_damageAmount} damage");
            if (!canTakeDamage || !isAlive) return;
            var damageToDeal = DamageWithArmourAdjustments(_damageAmount);
            Debug.Log($"Taking {damageToDeal} points of damage");
            currentHp.value -= damageToDeal;

            switch (damageToDeal) {
                case <= 15f: playerHurtSmall.Raise();
                    break;
                case > 15f and <= 50f: playerHurtMedium.Raise();
                    break;
                case > 50f when currentHp.value > 0f: playerHurtLarge.Raise();
                    break;
            }

            ControllerReferences.knockback.AddImpact(_other, knockbackForce);
            if (currentHp.value <= 0f) {
                Die();
            } else if (iFramesInSeconds > 0f) {
                AddIFrames(iFramesInSeconds);
            }
        }

        private float DamageWithArmourAdjustments(float _damageAmount) {
            float newDamage;
            switch (currentArmour.value) {
                case >= 100f: {
                    newDamage = _damageAmount * 0.5f;
                    var armourDamage = _damageAmount * 0.5f;
                    newDamage += CalculateRemainder(armourDamage);
                    break;
                }
                case < 100f when currentArmour.value != 0f: {
                    newDamage = _damageAmount * 0.666666f;
                    var armourDamage = _damageAmount * 0.333333f;
                    newDamage += CalculateRemainder(armourDamage);
                    break;
                }
                default: Debug.Log("No Armour Damage Taken");
                    newDamage = _damageAmount;
                    break;
            }
            return newDamage;
        }

        private float CalculateRemainder(float _armourDamage) {
            if (currentArmour.value >= _armourDamage) {
                Debug.Log($"Removing {_armourDamage} points of Armour");
                currentArmour.value -= _armourDamage;
                return 0f;
            } else {
                currentArmour.value = 0f;
                return _armourDamage - currentArmour.value;
            }
        }

        protected override void Die() {
            // PLAY DEATH FX
            Debug.Log("YOU R DED");
            canTakeDamage = false;
            GetComponent<Animator>().SetTrigger("Die");
            playerHurtKilled.Raise();
            // base.Die();
        }

        public void GoToDeathScreen() {
            // LoadS
            deathScreen.LoadScene();
        }

        #endregion

        #region Health & Armour Pickups

        public void AddHealth(float _amount) {
            currentHp.value += _amount;
        }

        public void PickupSuperHealth() {
            if (currentHp.value < 200f) {
                currentHp.value = 200f;
            }
        }

        public void PickupMedkit() {
            float remainder = maxHp.value - currentHp.value;
            if (remainder < 25f) {
                currentHp.value += remainder;
            } else {
                currentHp.value += 25f;
            }
        }

        public void PickupStim() {
            float remainder = maxHp.value - currentHp.value;
            if (remainder < 10f) {
                currentHp.value += remainder;
            } else {
                currentHp.value += 10f;
            }
        }

        public void PickupHealthBonus() {
            if (currentHp.value < 200f) {
                currentHp.value += 1f;
            }
        }

        public void PickupSuperAmour() {
            if (currentArmour.value < 200f) {
                currentArmour.value = 200f;
            }
        }

        public void PickupFullAmour() {
            if (currentArmour.value < 100f) {
                currentArmour.value = 100f;
            }
        }

        public void PickupSmallArmour() {
            float remainder = maxArmour.value - currentArmour.value;
            if (remainder < 25f) {
                currentArmour.value += remainder;
            } else {
                currentArmour.value += 10f;
            }
        }

        public void PickupArmourBonus() {
            if (currentArmour.value < 200f) {
                currentArmour.value += 1f;
            }
        }

        #endregion

        #region Return Functions

        public float GetMaxHealth() {
            return maxHp.value;
        }

        #endregion
    }
}