using HouseTrap.Core.GameManagement;
using UnityEngine;

namespace HouseTrap.Core.Controller {
    public class PlayerControllerRb : PlayerController {
        // VARIABLES //

        #region Variables

        // REFERENCES //
        private static Rigidbody rb;

        // MOVEMENT //
        [Header("Movement")]
        [SerializeField] private AnimationCurve moveCurve;
        private float moveTime;
        private float groundSpeedMultiplier = 10f;
        private float groundDrag = 6f;
        private float airSpeedMultiplier = 4f;
        private float airDrag = 2f;

        private Vector3 moveDirection = Vector3.zero;

        private Transform cameraHolderYaw;
        private Transform cameraHolderPitch;


        #endregion

        protected override void Start() {
            base.Start();
            rb = GetComponent<Rigidbody>();
            rb.freezeRotation = true;
            rb.interpolation = RigidbodyInterpolation.Interpolate;
            
            cameraHolderYaw = transform.GetChild(0);
            cameraHolderPitch = cameraHolderYaw.transform.GetChild(0);
            cam = ControllerReferences.cam;

            groundSpeedMultiplier = SettingsManager.ControllerSettings.GetWalkSpeedMultiplier();
            airSpeedMultiplier = SettingsManager.ControllerSettings.GetAirSpeedMultiplier();
            
            LockCursor();
        }

        private void Update() {
            if (!playerControllerEnabled) return;
            GetInput();
        }

        private void FixedUpdate() {
            if (!playerControllerEnabled) return;
            GetSpeedFromCurve();
            ControlDrag();
            MovePlayer();
        }

        private void LateUpdate() {
            if (!playerControllerEnabled) return;
            UpdateMouseLook();
        }

        // PLAYER MOVEMENT //

        #region Player Movement

        private void MovePlayer() {
            if (GroundDetection.CheckGrounded()) {
                rb.AddForce(moveDirection.normalized * (moveSpeed * groundSpeedMultiplier), ForceMode.Acceleration);
            } else {
                rb.AddForce(moveDirection.normalized * (moveSpeed * airSpeedMultiplier), ForceMode.Acceleration);
            }
        }

        private void GetSpeedFromCurve() {
            if (horizontalInput.magnitude > 0) {
                moveTime += Time.fixedDeltaTime;
            } else {
                moveTime = 0;
            }

            moveSpeed = moveCurve.Evaluate(moveTime);
        }

        private void ControlDrag() { rb.linearDamping = GroundDetection.CheckGrounded() ? groundDrag : airDrag; }

        #endregion

        // MOUSELOOK //

        #region Mouselook

        private void UpdateMouseLook() {
            var targetMouseDelta = mouseLookInput;
            
            currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseDeltaVelocity, mouseSmoothTime, Mathf.Infinity, Time.deltaTime);
            // Debug.Log(currentMouseDelta);

            cameraPitch -= currentMouseDelta.y * MouseSensitivity.y * mouseLookMultiplier;
            cameraPitch = Mathf.Clamp(cameraPitch, -90f, 90f);

            cameraHolderPitch.localRotation = Quaternion.Euler(cameraPitch, 0f, 0f);
            cameraHolderYaw.Rotate(Vector3.up * (currentMouseDelta.x * (MouseSensitivity.x * mouseLookMultiplier)));
        }

        #endregion

        #region Input

        private void GetInput() {
            horizontalInput = ControllerReferences.inputManager.GetHorizontalInput();
            moveDirection = cameraHolderYaw.forward * horizontalInput.y + cameraHolderYaw.right * horizontalInput.x;

            mouseLookInput = (ControllerReferences.inputManager.GetMouseLookInput());
        }

        public override void DisablePlayerController() {
            playerControllerEnabled = false;
            ControllerDisabled();
            currentMouseDelta = Vector2.zero;
            currentMouseDeltaVelocity = Vector2.zero;
            ControllerReferences.playerAnim.SetInteger("Run", 0);
        }
        public override void EnablePlayerController() {
            playerControllerEnabled = true;
            ControllerEnabled();
        }

        private void ControllerEnabled() { rb.WakeUp(); }
        private void ControllerDisabled() { rb.Sleep(); }

        #endregion
        
        #region Return Functions

        public static Rigidbody GetRigidbody() { return rb; }

        #endregion
    }
}