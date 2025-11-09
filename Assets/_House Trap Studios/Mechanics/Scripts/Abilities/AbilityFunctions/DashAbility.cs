using HouseTrap.Core;
using HouseTrap.Core.Controller;
using HouseTrap.Core.ScriptableVariables;
using UnityEngine;

namespace HouseTrap.BadThoughts {
    [CreateAssetMenu(menuName = "Ability/Dash")]
    public class DashAbility : AbilitySO {
        [SerializeField] private float dashForce;
        [SerializeField] private ScriptableVariableVector3 horizontalInput;
        private Vector3 dashDirection;
        [SerializeField] private float disableGravityTimer = .5f;

        public override void Activate() {
            Debug.Log("Dashing");
            // ControllerReferences.PlayerControllerCc.DisableGravity(disableGravityTimer);
            if (horizontalInput.value == Vector3.zero) {
                dashDirection = ControllerReferences.player.transform.forward;
            } else {
                dashDirection = ControllerReferences.player.transform.forward * horizontalInput.value.y + 
                                ControllerReferences.player.transform.right * horizontalInput.value.x;
            }

            ControllerReferences.knockback.AddImpact(dashDirection, dashForce, ForceMode.Impulse);
        }
    }
}