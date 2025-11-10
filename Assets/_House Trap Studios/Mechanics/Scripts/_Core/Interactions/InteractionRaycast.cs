using HouseTrap.Core.GameManagement;
using UnityEngine;
using UnityEngine.UI;

namespace HouseTrap.Core.Interactions {
    public class InteractionRaycast : MonoBehaviour {
        private float interactionDistance;
        [SerializeField] private LayerMask targetableLayers;

        [SerializeField] private Transform interactableIcon;
        [SerializeField] private RawImage interactableIconRawImage;
        private UIHoverAtWorldPosition interactableIconHover;

        private bool canInteract = true;
        private bool interactTrigger;

        private void Start() {
            interactionDistance = SettingsManager.ControllerSettings.GetInteractionDistance();
            interactableIcon = GameObject.FindWithTag("InteractUI").transform;
            interactableIconRawImage = interactableIcon.GetComponent<RawImage>();
            interactableIconHover = interactableIcon.GetComponent<UIHoverAtWorldPosition>();

            Debug.Log(interactableIconRawImage != null ? "Interact UI Set Successfully" : "*Interact UI NOT SET*");
        }

        private void Update() {
            if (!GetInteractable()) {
                ResetUI();
            }
        }

        private bool GetInteractable() {
            if (!Physics.Raycast(this.transform.position, transform.forward, out var hit, interactionDistance,
                    targetableLayers) || !canInteract) return false;
            if (hit.collider.CompareTag("Interactable")) {
                interactableIconHover.SetHoverTransform(hit.transform);
                interactableIconRawImage.color = new Color32(255, 255, 255, 255);

                if (!interactTrigger) return true;
                hit.collider.GetComponent<IInteractable>().Interact();
                StopInteract();

                return true;
            }

            return false;
        }

        private void ResetUI() { interactableIconRawImage.color = new Color32(255, 255, 255, 0); }

        public void TriggerInteract() {
            if (!canInteract) return;
            interactTrigger = true;
            // Debug.Log("Interaction Triggered");
            Invoke(nameof(StopInteract), 0.1f);
        }

        private void StopInteract() {
            if (interactTrigger) {
                interactTrigger = false;
                // Debug.Log("Interaction Ended");
            }
        }

        public void EnableInteraction() { canInteract = true; }

        public void DisableInteraction() { canInteract = false; }
    }
}