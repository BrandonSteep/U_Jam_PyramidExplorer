using HouseTrap.Core;
using HouseTrap.Core.Controller;
using HouseTrap.Core.EventSystem;
using HouseTrap.Core.ScriptableVariables;
using UnityEngine;

namespace HouseTrap.BadThoughts {
    public class AbilityHolder : MonoBehaviour {
        private float abilityInput;
        
        public AbilitySO equippedAbility;
        [SerializeField] private CurrentlyActiveAbility activeAbility;
        public AbilitySO newAbility;

        [SerializeField] private ScriptableVariableFloat cooldownTimer;
        [SerializeField] private GameEvent abilityReady;
        [SerializeField] private GameEvent abilitySwapped;

        [SerializeField] private AudioSource aSource;
        [SerializeField] private AudioClip rechargedSfx;

        public bool isReady;

        public void EquipAbility() {
            if (newAbility && newAbility != equippedAbility) {
                equippedAbility = newAbility;
            }

            isReady = true;
            activeAbility.activeAbility = equippedAbility;
            abilitySwapped.Raise();
            Debug.Log("Ability Equipped");
        }

        private void Update() {
            if (!equippedAbility.cooldownTimer) return;
            abilityInput = ControllerReferences.inputManager.GetAbility();
            
            if (cooldownTimer.value < equippedAbility.cooldownTimer.value) {
                cooldownTimer.value += Time.deltaTime;
            } else if (!isReady) {
                isReady = true;
                aSource.PlayOneShot(rechargedSfx);
                abilityReady.Raise();
            }
            
            if(abilityInput > 0 && isReady) Activate();
        }

        private void FixedUpdate() {
            if (equippedAbility.triggerOnUpdate) {
                equippedAbility.RunScript();
            }
        }

        public void Activate() {
            if (!isReady) return;
            Debug.Log("Activating Ability");
            isReady = false;
            equippedAbility.Activate();
            cooldownTimer.value = 0f;

            aSource.PlayOneShot(equippedAbility.activateSfx);
            // StartCoroutine("Cooldown");
        }

        // private IEnumerator Cooldown(){
        //     yield return new WaitForSeconds(_equippedAbility._cooldownTimer);
        //     _isReady = true;
        //     yield return null;
        // }

        private void OnEnable() {
            aSource = GetComponent<AudioSource>();
            EquipAbility();
        }
    }
}