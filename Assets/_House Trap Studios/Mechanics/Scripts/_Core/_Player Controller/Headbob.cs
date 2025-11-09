using HouseTrap.Audio;
using UnityEngine;

namespace HouseTrap.Core.Controller {
    public class Headbob : MonoBehaviour {
        [SerializeField] private bool showDebugMessages;
        [SerializeField] private AudioClip[] footstepSounds;
        [SerializeField] private float animSpeedMultiplier = 0.25f;

        private Animator anim;

        private void Awake() { anim = GetComponentInParent<Animator>(); }
        private void Update() { Bob(); }

        // ReSharper disable Unity.PerformanceAnalysis
        private void Bob() {
            if (!GroundDetection.CheckGrounded()) return;
            anim.SetBool("Falling", false);
            if (ControllerReferences.inputManager.GetHorizontalInput().magnitude > 0) {
                if (showDebugMessages) {
                    Debug.Log($"Running");
                }

                SetRunSpeed();
                anim.SetInteger("Run", 1);
            } else {
                anim.SetInteger("Run", 0);
            }
        }

        public void PlayFootstep() {
            AudioManager.PlayOneShot(footstepSounds[UnityEngine.Random.Range(0, footstepSounds.Length)]);
        }

        private void SetRunSpeed() {
            var multiplier = ControllerReferences.PlayerController.GetPlayerSpeed() * animSpeedMultiplier;
            // Debug.Log($"Running at {multiplier}");
            anim.SetFloat("RunSpeed", multiplier);
        }
    }
}