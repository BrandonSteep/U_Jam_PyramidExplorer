using HouseTrap.Audio;
using HouseTrap.Core.Controller;
using HouseTrap.Core.EventSystem;
using UnityEngine;

namespace HouseTrap.Core.Interactions.Pickups {
    public class Medkit : MonoBehaviour {
        [SerializeField] private float healthToAdd = 25f;
        [SerializeField] private float collectLimit = 100f;
        [SerializeField] private GameEvent collectEvent;

        [SerializeField] private AudioClip pickupSound;

        private void OnTriggerEnter(Collider _other) {
            // Debug.Log("Triggered lol");
            if (_other.CompareTag("Player") && ControllerReferences.playerStatus.currentHp.value < collectLimit) {
                PickUp();
            }
        }

        private void PickUp() {
            var remainder = collectLimit - ControllerReferences.playerStatus.currentHp.value;
            if (remainder < healthToAdd) {
                Debug.Log($"Medkit Picked Up - Adding {remainder} to Player HP");
                ControllerReferences.playerStatus.AddHealth(remainder);
            } else {
                Debug.Log($"Medkit Picked Up - Adding {healthToAdd} to Player HP");
                ControllerReferences.playerStatus.currentHp.value += healthToAdd;
            }

            collectEvent.Raise();
            AudioManager.PlayOneShot(pickupSound);
            Destroy(gameObject);
        }
    }
}