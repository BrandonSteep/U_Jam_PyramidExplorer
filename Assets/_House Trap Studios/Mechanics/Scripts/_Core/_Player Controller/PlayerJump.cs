using UnityEngine;
using HouseTrap.Audio;

namespace HouseTrap.Core.Controller {
    public class PlayerJump : MonoBehaviour {
        private float jumpInput;
        
        [SerializeField] private float jumpForce = 5f;
        
        private bool betterJumpEnabled = true;
        [SerializeField] private float jumpCooldown = 0.15f;
        [SerializeField] private bool jumpCooldownActive;
        [SerializeField] private float fallMultiplier = 2.5f;
        [SerializeField] private float lowJumpMultiplier = 2.5f;
        [SerializeField] private bool isFalling;
        
        private Rigidbody rb;
        
        // Coyote & Buffer
        [SerializeField] private float coyoteTime;
        private bool canJump;
        [SerializeField] private float bufferTime;
        private bool jumpTrigger;
        
        // FX
        [SerializeField] private Animator anim;
        [SerializeField] private GameObject jumpParticles;
        [SerializeField] private AudioClip jumpSound;
        [SerializeField] private AudioClip landSound;
        
        private void Awake() {
            rb = GetComponent<Rigidbody>();
            anim = GetComponent<Animator>();
        }
        
        private void Update() {
            GetInput();
            CoyoteTime();
            if (jumpInput > 0f && !jumpCooldownActive) {
                TriggerJump();
            }
        }
        
        private void FixedUpdate() {
            if (jumpTrigger && canJump) {
                Jump();
            }
        
            BetterJump();
        }
        
        private void GetInput() { jumpInput = ControllerReferences.inputManager.GetAbility(); }
        
        private void Jump() {
            jumpTrigger = false;
            canJump = false;
            jumpCooldownActive = true;
            Invoke(nameof(DisableJumpCooldown), jumpCooldown);
        
            CancelInvoke(nameof(CancelJump));
            // Debug.Log("Jump");
        
            anim.SetTrigger("Jump");
        
            ResetVerticalVelocity();
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        
            AudioManager.PlayOneShot(jumpSound);
        }
        
        private void ResetVerticalVelocity() {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
            betterJumpEnabled = false;
            Invoke(nameof(EnableBetterJump), jumpCooldown);
        }
        
        private void DisableJumpCooldown() { jumpCooldownActive = false; }
        private void EnableBetterJump() { betterJumpEnabled = true; }
        
        private void BetterJump() {
            if (!betterJumpEnabled) return;
            switch (rb.linearVelocity.y) {
                case < -0.1f and > -20f:
                    rb.linearVelocity += Vector3.up * (Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime);
                    if (!isFalling) {
                        anim.SetBool("Falling", true);
                    }
        
                    isFalling = true;
                    break;
                case < -20f: {
                    rb.linearVelocity += Vector3.up * (Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime);
                    if (isFalling == GroundDetection.CheckGrounded()) {
                        // AudioManager.PlayOneShot(landSound);
                        // PlayDust();
                        isFalling = false;
                    }
        
                    break;
                }
                case > 1f when jumpInput < 1f: {
                    rb.linearVelocity += Vector3.up * (Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime);
                    isFalling = false;
                    break;
                }
            }
        }
        
        // Coyote Time
        private void CoyoteTime() {
            if (!GroundDetection.CheckGrounded() && canJump) {
                Invoke(nameof(DisableJump), coyoteTime);
            } else if (GroundDetection.CheckGrounded() && !jumpCooldownActive) {
                CancelInvoke(nameof(DisableJump));
                if (!canJump) {
                    AudioManager.PlayOneShot(landSound);
                    anim.ResetTrigger("Jump");
                    anim.SetBool("Falling", false);
                    anim.SetTrigger("Land");
                }
        
                canJump = true;
            }
        }
        
        private void DisableJump() { canJump = false; }
        
        // Jump Buffer
        private void TriggerJump() {
            jumpTrigger = true;
            Invoke(nameof(CancelJump), bufferTime);
        }
        
        private void CancelJump() {
            if (!GroundDetection.CheckGrounded()) {
                jumpTrigger = false;
            }
        }
    }
}