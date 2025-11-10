using UnityEngine;

namespace HouseTrap.Core {
    public class GroundDetection : MonoBehaviour {
        public static GroundDetection Instance;
        private static bool showDebugMessages = false;

        public static bool IsGrounded;
        [SerializeField] private LayerMask groundLayers;

        [SerializeField] private float raycastLength;
        private Vector3 raycastOffset;

        private bool groundCollision;

        private void Awake() {
            Instance = this;
            raycastOffset = new Vector3(0f, raycastLength, 0f);
            GetComponentInParent<Animator>();
        }

        private void Update() { DetectGround(); }

        private void DetectGround() {
            IsGrounded = Physics.Raycast(transform.position + raycastOffset, Vector3.down, raycastLength * 2,
                groundLayers);

            // Add Any Extra checks here //
            if (!IsGrounded) {
                IsGrounded = CheckSphere();
            }
        }

        private bool CheckSphere() {
            var others = Physics.OverlapSphere(transform.position, 0.45f);
            if (others.All(_other => (groundLayers & (1 << _other.gameObject.layer)) == 0)) return false;

            if (showDebugMessages) {
                Debug.Log($"Touching Grass");
            }

            return true;
        }

        public static bool CheckGrounded() {
            if (showDebugMessages) {
                Debug.Log($"Is Grounded = {IsGrounded}");
            }

            return IsGrounded;
        }
    }
}