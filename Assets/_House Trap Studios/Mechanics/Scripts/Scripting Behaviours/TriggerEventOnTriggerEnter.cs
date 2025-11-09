using UnityEngine;

namespace HouseTrap.Core.EventSystem {
    public class TriggerEventOnTriggerEnter : GameEventTrigger {
        void OnTriggerEnter(Collider _other) { TriggerEvent(); }
    }
}