using UnityEngine;
using UnityEngine.Events;

namespace HouseTrap.Core.EventSystem {
    public class GameEventTrigger : MonoBehaviour {
        public UnityEvent EventToCall;

        public void TriggerEvent() { EventToCall.Invoke(); }
    }
}