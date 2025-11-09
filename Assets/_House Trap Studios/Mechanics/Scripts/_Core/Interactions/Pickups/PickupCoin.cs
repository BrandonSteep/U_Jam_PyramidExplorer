using HouseTrap.Audio;
using HouseTrap.Core.Controller;
using HouseTrap.Core.EventSystem;
using UnityEngine;

namespace HouseTrap.Core.Interactions.Pickups {
    public class PickupCoin : MonoBehaviour, IInteractable {
        private Collider coll;
        [SerializeField] private LayerMask collectMask;
        [SerializeField] private Transform model;

        [SerializeField] private GameEvent triggerOnCollection;
        [SerializeField] private GameObject collectionParticles;
        [SerializeField] private AudioClip collectionAudio;

        private void Awake() { coll = GetComponent<Collider>(); }

        private void OnTriggerEnter(Collider _other) {
            if (_other.gameObject == ControllerReferences.player) {
                Interact();
            }
        }

        public void Interact() {
            coll.enabled = false;
            triggerOnCollection.Raise();
            Instantiate(collectionParticles, model.position, Quaternion.identity);
            AudioManager.PlayOneShot(collectionAudio);
            Destroy(gameObject);
        }
    }
}