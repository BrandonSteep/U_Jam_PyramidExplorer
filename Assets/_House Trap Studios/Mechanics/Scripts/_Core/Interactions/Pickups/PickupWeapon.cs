using HouseTrap.Audio;
using HouseTrap.Core.Controller;
using UnityEngine;

namespace HouseTrap.Core.Interactions.Pickups {
    public class PickupWeapon : MonoBehaviour, IInteractable {
        [SerializeField] private BoomerShooterWeaponSystem equippedManager;
        [SerializeField] private int weaponIndex;
        [SerializeField] private AudioClip pickupSound;

        private void Start() {
            Debug.Log($"Setting equippedManager as {ControllerReferences.equipmentManager}");
            equippedManager = ControllerReferences.equipmentManager;
        }

        private void OnTriggerEnter(Collider _other) {
            if (_other.gameObject == ControllerReferences.player) {
                Interact();
            }
        }

        public void Interact() {
            Debug.Log(equippedManager);
            equippedManager.PickupWeapon(weaponIndex);
            if (pickupSound) {
                AudioManager.PlayOneShot(pickupSound);
            }

            Destroy(gameObject);
        }
    }
}