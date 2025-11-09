using HouseTrap.BadThoughts;
using HouseTrap.Core.Controller;
using UnityEngine;

namespace HouseTrap.Core.Interactions.Pickups {
    public class AbilityPickup : MonoBehaviour, IInteractable {
        [SerializeField] private AbilitySO abilityToPickUp;

        [SerializeField] private AudioClip pickupSound;
        [SerializeField] private AudioSource aSource;

        public void Interact() {
            ControllerReferences.abilityHolder.newAbility = abilityToPickUp;
            ControllerReferences.abilityHolder.EquipAbility();
            if (pickupSound) {
                aSource.PlayOneShot(pickupSound);
            }
        }
    }
}