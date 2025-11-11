using HouseTrap.Core.Controller;
using UnityEngine;

namespace HouseTrap.Core {
    public class KnockbackRb : MonoBehaviour, IKnockback {
        public IKnockback Instance;
        private Rigidbody rb;

        private void Start() {
            Instance = this;
            rb = GetComponent<Rigidbody>();
        }

        public void AddImpact(Vector3 _direction, float _forceAmount, ForceMode _forceMode) {
            rb.AddForce(_direction * _forceAmount, _forceMode);
        }

        public void AddImpact(Transform _other, float _forceAmount) {
            var dir = (_other.position - transform.position).normalized;
            AddImpact(-dir, _forceAmount, ForceMode.Impulse);
        }
    }
}