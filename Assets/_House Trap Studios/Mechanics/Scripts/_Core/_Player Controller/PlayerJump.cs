using UnityEngine;
using HouseTrap.Audio;
using HouseTrap.Core.GameManagement;

namespace HouseTrap.Core.Controller {
    public class PlayerJump : MonoBehaviour {
        private float jumpInput;
        private bool betterJumpEnabled = true;
        private bool hasJumpedOnInput;
        private float jumpForce = 5f;
        private float jumpCooldown = 0.15f;
        private bool jumpCooldownActive;
        private float fallMultiplier = 5f;
        private float lowJumpMultiplier = 5f;
        
        [Header("Jump Settings Are Controlled By Settings Manager")]
        [SerializeField] private bool isFalling;
        
        private Rigidbody rb;
        
        // Coyote & Buffer
        private float coyoteTime;
        private bool canJump;
        
        private float bufferTime;
        private bool jumpTrigger;
        
        // FX
        [SerializeField] private Animator anim;
        [SerializeField] private GameObject jumpParticles;
        [SerializeField] private AudioClip jumpSound;
        [SerializeField] private AudioClip landSound;
        
        private void Awake() {
            rb = GetComponent<Rigidbody>();
            anim = GetComponent<Animator>();
            
            jumpForce = SettingsManager.ControllerSettings.GetJumpForce();
            betterJumpEnabled = SettingsManager.ControllerSettings.GetBetterJumpEnabled();
            jumpCooldown = SettingsManager.ControllerSettings.GetJumpCooldown();
            fallMultiplier = SettingsManager.ControllerSettings.GetFallMultiplier();
            lowJumpMultiplier = SettingsManager.ControllerSettings.GetLowJumpMultiplier();
            coyoteTime = SettingsManager.ControllerSettings.GetCoyoteTime();
            bufferTime = SettingsManager.ControllerSettings.GetBufferTime();
        }
        
        private void Update() {
            GetInput();
            CoyoteTime();
            if (jumpInput > 0f && !jumpCooldownActive && !hasJumpedOnInput) {
                TriggerJump();
            }
            else if (hasJumpedOnInput && jumpInput == 0f) {
                hasJumpedOnInput = false;
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
            hasJumpedOnInput = true;
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
            // Debug.Log($"Linear Velocity Y: {rb.linearVelocity.y} & Falling: {isFalling}");
            switch (rb.linearVelocity.y) {
                case < -0.1f and > -20f:
                    rb.linearVelocity += Vector3.up * (Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime);
                    if (!GroundDetection.CheckGrounded() && !isFalling) {
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
                isFalling = false;
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