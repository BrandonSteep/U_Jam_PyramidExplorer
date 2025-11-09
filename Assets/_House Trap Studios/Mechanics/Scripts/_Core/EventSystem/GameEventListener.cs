using UnityEngine;
using UnityEngine.Events;

namespace HouseTrap.Core.EventSystem {
    public class GameEventListener : MonoBehaviour {
        public GameEvent Event;
        public UnityEvent Response;

        public void OnEventRaised() { Response.Invoke(); }

        private void OnEnable() => Event.RegisterListener(this);

        private void OnDisable() => Event.UnregisterListener(this);
    }
}