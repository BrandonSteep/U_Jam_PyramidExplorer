using HouseTrap.Core;
using HouseTrap.Core.Controller;
using UnityEngine;

namespace HouseTrap.BadThoughts {
    [CreateAssetMenu(menuName = "Ability/Jump")]
    public class JumpAbility : AbilitySO {

        [SerializeField] private bool canJump;
        [SerializeField] private float jumpForce = 125f;

        [SerializeField] private float jumpBufferTime = 0.2f;

        private float jumpBufferCurrent;

        // [SerializeField] private float fallMultiplier = 2.5f;
        [SerializeField] private float gravityDisableTime = 0.2f;

        public override void Activate() {
            Debug.Log("Jump Activated");
            jumpBufferCurrent = jumpBufferTime;
            HandleJump();
        }

        #region Jump Logic

        public override void RunScript() {
            if (ControllerReferences.isGrounded) {
                ResetJump();
            }
        }

        private void HandleJump() {
            jumpBufferCurrent -= Time.deltaTime;

            if (!(jumpBufferCurrent > 0) || !canJump) return;
            canJump = false;
            // Debug.Log("Jump");
            // ControllerReferences.PlayerControllerCc.DisableGravity(gravityDisableTime);
            ControllerReferences.knockback.AddImpact(Vector3.up, jumpForce, ForceMode.Impulse);
        }

        // private void BetterJump()
        // {
        //     if(ControllerReferences.playerController.controller.velocity.y < -1.5 && !ControllerReferences.playerController.controller.isGrounded)
        //     {
        //         ControllerReferences.playerController.velocityY += ControllerReferences.playerController.gravity * fallMultiplier / 10f;
        //     }
        // }

        void ResetJump() {
            if (!canJump) {
                canJump = true;
            }
        }

        #endregion
    }
}