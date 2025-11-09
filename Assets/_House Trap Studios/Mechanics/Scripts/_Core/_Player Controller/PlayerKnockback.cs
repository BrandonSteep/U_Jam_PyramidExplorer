using UnityEngine;

namespace HouseTrap.Core.Controller {
    public class KnockbackCc : MonoBehaviour, IKnockback {
        private float mass = 3.0f; // Player's Mass
        private Vector3 impact = Vector3.zero;
        private CharacterController player;

        private void Start() {
            player = GetComponent<CharacterController>();
        }

        private void FixedUpdate() {
            // apply the impact force:
            if (!(impact.magnitude > 0.2)) return;
            player.Move(impact * Time.deltaTime);

            // consumes the impact energy each cycle:
            impact = Vector3.Lerp(impact, Vector3.zero, 5 * Time.deltaTime);
        }

        public void AddImpact(Vector3 _direction, float _forceAmount, ForceMode _forceMode) {
            // Debug.Log($"Add Impact: {dir}");

            _direction.Normalize();
            if (_direction.y < 0) _direction.y = -_direction.y; // reflect down force on the ground
            impact += _direction.normalized * _forceAmount / mass;
        }
        
        public void AddImpact(Transform _other, float _forceAmount) {
            var dir = (_other.position - transform.position).normalized;
            AddImpact(-dir, _forceAmount, ForceMode.Impulse);
        }

        public void PushPlayerBack() {
            AddImpact(ControllerReferences.cam.transform.forward, -50f, ForceMode.Impulse);
        }
    }
}