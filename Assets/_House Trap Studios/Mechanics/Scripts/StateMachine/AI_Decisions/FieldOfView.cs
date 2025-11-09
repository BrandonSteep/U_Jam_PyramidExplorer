using UnityEngine;

namespace HouseTrap.BadThoughts {
    public class FieldOfView : MonoBehaviour {
        [SerializeField] private float radius;
        [Range(0, 360)] [SerializeField] private float angle;
        private bool canSeePlayer;

        [SerializeField] private LayerMask targetMask;
        [SerializeField] private LayerMask obstructionMask;

        public bool CheckFOV(AIStateMachineManager sm) {

            // var thisPosition = sm.transform.position + Vector3.up * 1f;

            Collider[] rangeChecks = Physics.OverlapSphere(sm.transform.position, radius, targetMask);

            if (rangeChecks.Length != 0) {
                Vector3 directionToTarget = (sm.GetTarget().transform.position - sm.transform.position).normalized;

                if (Vector3.Angle(sm.transform.forward, directionToTarget) < angle * 0.5) {
                    // Debug.Log("Running Enemy FOV");

                    float distanceToTarget = Vector3.Distance(sm.transform.position, sm.GetTarget().transform.position);

                    if (!Physics.Raycast(sm.transform.position, directionToTarget, distanceToTarget, obstructionMask)) {
                        // Debug.Log("FoV Success!");
                        canSeePlayer = true;
                        return true;
                    } else {
                        canSeePlayer = false;
                        return false;
                    }
                } else {
                    canSeePlayer = false;
                    return false;
                }
            } else {
                canSeePlayer = false;
                return false;
            }
        }

        public float CheckRadius() {
            return radius;
        }

        public float CheckAngle() {
            return angle;
        }

        public bool CheckPlayerSight() {
            return canSeePlayer;
        }
    }
}