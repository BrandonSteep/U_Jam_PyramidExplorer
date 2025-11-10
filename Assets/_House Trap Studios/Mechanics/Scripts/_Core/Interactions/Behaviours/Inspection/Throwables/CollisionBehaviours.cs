using HouseTrap.Audio;
using UnityEngine;

namespace HouseTrap.Core.Interactions.Inspection.PhysicsObject {
    public class CollisionBehaviours : MonoBehaviour {
        [SerializeField] protected float smashForce = 10f;
        [SerializeField] protected AudioClip smashSound;
        private InteractionInspectPickUp interactable;
        private ItemSpawner itemSpawnerSpawner;
        
        private void Awake() {
            interactable = GetComponent<InteractionInspectPickUp>();
            itemSpawnerSpawner = GetComponent<ItemSpawner>();
        }

        private void OnCollisionEnter(Collision _other) {
            if(_other.relativeVelocity.magnitude >= smashForce)
                OnSmash(_other);
            else
                OnTap(_other);
        }

        protected virtual void OnSmash(Collision _other) {
            Debug.Log($"Object SMASHED against {_other.gameObject.name}");
            AudioManager.PlayOneShot(smashSound);
            InventoryReferences.ObjectPool.SpawnFromPool("SmashGeneric", transform.position, Quaternion.identity);

            if (interactable.GetIsInspecting()) {
                interactable.CallStopInspecting();
            }

            if (itemSpawnerSpawner) {
                itemSpawnerSpawner.SpawnItem();
            }
            
            Destroy(gameObject);
        }

        protected virtual void OnTap(Collision _other) {
            Debug.Log($"Object tapped against {_other.gameObject.name}");
            
        }
    }
}