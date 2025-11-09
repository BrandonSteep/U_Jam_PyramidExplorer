using HouseTrap.Core.EventSystem;
using UnityEngine;

namespace HouseTrap.Core.Interactions {
    public class InteractionTriggerEvent : InteractionTextPopup {
        [SerializeField] private GameEvent eventToTrigger;

        public override void Interact() {
            eventToTrigger.Raise();
            base.Interact();
        }
    }
}