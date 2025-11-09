using HouseTrap.Core;
using HouseTrap.Core.Controller;
using UnityEngine;

namespace HouseTrap.BadThoughts {
    public class AimAtTarget : MonoBehaviour {
        [SerializeField] private AIStateMachineManager sm;
        [SerializeField] private bool inverted;

        [SerializeField] private bool lookAtPlayerCamera;

        private void Awake() {
            if (!sm) {
                sm = GetComponentInParent<AIStateMachineManager>();
            }

            InvokeRepeating(nameof(Look), 0f, 0.1f);
        }

        private void Look() {
            if (sm.GetTarget() && !lookAtPlayerCamera) {
                transform.LookAt(2 * transform.position - sm.GetTarget().transform.position);
            } else if (lookAtPlayerCamera) {
                transform.LookAt(2 * transform.position - ControllerReferences.cam.transform.position);
            }
        }
    }
}