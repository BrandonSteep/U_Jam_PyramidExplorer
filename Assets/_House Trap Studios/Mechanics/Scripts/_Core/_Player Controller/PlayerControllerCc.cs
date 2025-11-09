namespace HouseTrap.Core.Controller {
    public class PlayerControllerCc : PlayerController {
        // // VARIABLES //
        //
        // #region Variables
        // // REFERENCES //
        // private CharacterController controller;
        //
        // // M O V E M E N T //
        // private float currentWalkSpeed;
        // private bool canRun = true;
        // private Vector2 currentDir = Vector2.zero;
        // private Vector2 currentDirVelocity = Vector2.zero;
        //
        // // M O U S E   L O O K //
        // //[SerializeField] float aimSpeed = 2.0f;
        //
        //
        // // GRAVITY //
        // [Header("Physics")] private float velocityY;
        // private bool gravityEnabled = true;
        //
        // #endregion
        //
        // // FRAME-BASED ///
        //
        // #region Frame-Based Methods
        //
        // protected override void Start() {
        //     UpdateControllerSettings();
        //     UpdatePlayerSettings();
        //     controller = GetComponent<CharacterController>();
        //
        //     if (controllerSettings.GetLockCursor()) {
        //         Cursor.lockState = CursorLockMode.Locked;
        //         Cursor.visible = false;
        //     }
        //
        //     currentWalkSpeed = controllerSettings.GetWalkSpeed(); // Add Item-Based Walk Speed Adjustment Here
        //
        //     controllerSettings.GetCurrentStaminaSO().value = controllerSettings.GetMaxStaminaSO().value;
        // }
        //
        // private void FixedUpdate() {
        //     UpdateGravity();
        //     if (playerControllerEnabled) {
        //         // UpdateMouseLook();
        //         UpdateMovement();
        //     }
        //
        //     UpdateStamina();
        //
        //     //Debug.Log("stamina = " + stamina);
        // }
        //
        // private void Update() {
        //     if (!playerControllerEnabled) return;
        //     ReceiveInput();
        //     UpdateMouseLook();
        // }
        //
        // #endregion
        //
        // // MOVEMENT //
        //
        // #region Keyboard & Mouse Controls
        //
        // private void UpdateMovement() {
        //     var targetDir = horizontalInput;
        //     targetDir.Normalize();
        //
        //     currentDir = Vector2.SmoothDamp(currentDir, targetDir, ref currentDirVelocity,
        //         controllerSettings.GetMoveSmoothTime());
        //
        //     if (Mathf.Approximately(aiming, 1f)) {
        //         currentWalkSpeed *= controllerSettings.GetAimWalkSpeedMultiplier();
        //     }
        //
        //     var velocity = (transform.forward * currentDir.y + transform.right * currentDir.x) * currentWalkSpeed +
        //                    Vector3.up * velocityY;
        //
        //     controller.Move(velocity * Time.deltaTime);
        //
        //     if (horizontalInput != new Vector2(0, 0)) {
        //         // Animation & Running //
        //         UpdateRunning();
        //
        //         if (OnSlope()) {
        //             controller.Move(Vector3.down * (controller.height * 0.5f * controllerSettings.GetSlopeForce() * Time.deltaTime));
        //         }
        //     } else {
        //         ControllerReferences.playerAnim.SetInteger("Walking", 0);
        //     }
        //
        // }
        //
        // // MOUSE LOOK //
        // private void UpdateMouseLook() {
        //     var targetMouseDelta = mouseLookInput;
        //
        //     // Debug.Log($"targetMouseDelta = {targetMouseDelta}");
        //
        //     currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseDeltaVelocity,
        //         mouseSmoothTime);
        //
        //     cameraPitch -= currentMouseDelta.y * MouseSensitivity.y * mouseLookMultiplier;
        //     cameraPitch = Mathf.Clamp(cameraPitch, -85.0f, 85.0f);
        //
        //     cam.transform.localEulerAngles = Vector3.right * cameraPitch;
        //
        //     transform.Rotate(Vector3.up * (currentMouseDelta.x * (MouseSensitivity.x * mouseLookMultiplier)));
        // }
        //
        // #endregion
        //
        // // RUNNING //
        //
        // #region Running
        //
        // private void UpdateRunning() {
        //     if (Mathf.Approximately(running, 1) && canRun) {
        //         ControllerReferences.playerAnim.SetInteger("Walking", 2);
        //         currentWalkSpeed = controllerSettings.GetWalkSpeed() * controllerSettings.GetRunSpeedMultiplier();
        //         controllerSettings.GetCurrentStaminaSO().value -=
        //             Time.deltaTime * controllerSettings.GetStaminaDrainRate();
        //         if (controllerSettings.GetCurrentStaminaSO().value <= 0) {
        //             canRun = false;
        //         } else if (Mathf.Approximately(controllerSettings.GetCurrentStaminaSO().value, controllerSettings.GetMaxStaminaSO().value)) {
        //             canRun = true;
        //         }
        //     } else {
        //         ControllerReferences.playerAnim.SetInteger("Walking", 1);
        //         currentWalkSpeed = controllerSettings.GetWalkSpeed();
        //     }
        // }
        //
        // private void UpdateStamina() {
        //     if (Mathf.Approximately(running, 1) && horizontalInput != new Vector2(0, 0) && canRun) return;
        //     
        //     if (controllerSettings.GetCurrentStaminaSO().value < controllerSettings.GetMaxStaminaSO().value) {
        //         controllerSettings.GetCurrentStaminaSO().value +=
        //             Time.deltaTime * controllerSettings.GetStaminaRefillRate();
        //     } else {
        //         canRun = true;
        //     }
        // }
        //
        // #endregion
        //
        // // PHYSICS //
        //
        // #region Physics
        //
        // // GRAVITY //
        // void UpdateGravity() {
        //     if (controller.isGrounded) {
        //         velocityY = 0.0f;
        //     }
        //     else if (gravityEnabled) {
        //         velocityY += controllerSettings.GetGravity() * Time.deltaTime;
        //     }
        // }
        //
        // public void DisableGravity(float _time) {
        //     gravityEnabled = false;
        //     Invoke(nameof(EnableGravity), _time);
        // }
        //
        // public void EnableGravity() {
        //     gravityEnabled = true;
        // }
        //
        //
        // // SLOPE CHECK //
        // private bool OnSlope() {
        //     RaycastHit hit;
        //     if (Physics.Raycast(transform.position, Vector3.down, out hit,
        //             controller.height / 2 * controllerSettings.GetSlopeForceRayLength())) {
        //         if (hit.normal != Vector3.up) {
        //             return true;
        //         } else {
        //             return false;
        //         }
        //     } else {
        //         return false;
        //     }
        //
        // }
        //
        // #endregion
        //
        //
        // // INPUT //
        //
        // #region Input
        //
        // private void ReceiveInput() {
        //     var input = ControllerReferences.inputManager.GetInput();
        //     horizontalInput = input.horizontalInput;
        //     mouseLookInput = input.mouseLookInput;
        //     running = input.runningInput;
        //     aiming = input.aimingInput;
        // }
        //
        // public void ControllerEnabled() {
        //     controller.enabled = true;
        // }
        //
        // public void ControllerDisabled() {
        //     controller.enabled = false;
        // }
        //
        // public override void DisablePlayerController() {
        //     currentWalkSpeed = 0f;
        //     ControllerReferences.playerAnim.SetInteger("Walking", 0);
        // }
        //
        // public override void EnablePlayerController() {
        //     playerControllerEnabled = true;
        // }
        //
        // #endregion
        //
        // #region Return Functions
        //
        // public Vector2 GetHorizontalInput() { return horizontalInput; }
        // public Vector2 GetMouseLookInput() { return mouseLookInput; }
        // public CharacterController GetCharacterController() { return controller; }
        //
        // #endregion
        //
        // public void Interact() {
        //     cam.GetComponent<InteractionRaycast>().TriggerInteract();
        // }
        //
        // #region Settings
        //
        // // C O N T R O L L E R   S E T T I N G S
        // private ControllerSettingsSO controllerSettings;
        //
        // private void UpdateControllerSettings() {
        //     controllerSettings = SettingsManager.controllerSettings;
        // }
        //
        // #endregion
    }
}