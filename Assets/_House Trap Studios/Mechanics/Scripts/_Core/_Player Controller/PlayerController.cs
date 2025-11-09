using HouseTrap.Core.GameManagement;
using UnityEngine;

namespace HouseTrap.Core.Controller {
    public class PlayerController : MonoBehaviour {
        #region Variables
        
        protected Camera cam;
        
        // INPUT //
        [Header("Input")]
        protected bool playerControllerEnabled = true;
        protected Vector2 horizontalInput;
        protected Vector2 mouseLookInput;
        protected float running;
        protected float aiming;
        
        protected Vector2 currentMouseDelta;
        protected Vector2 currentMouseDeltaVelocity = Vector2.zero;
        
        // MOVEMENT //
        protected float moveSpeed;
        
        // MOUSELOOK //
        [Header("Mouselook")]
        protected float cameraPitch = 0.0f;
        protected static Vector2 MouseSensitivity = new Vector2(35f, 35f);
        [SerializeField] protected float mouseLookMultiplier = 0.1f;
        [SerializeField] [Range(0.0f, 0.5f)] protected float mouseSmoothTime = 0.03f;
        
        #endregion
        protected virtual void Start() {
            UpdateControllerSettings();
            UpdatePlayerSettings();
        }

        public static void LockCursor() {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        public static void UnlockCursor() {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        
        public virtual void DisablePlayerController() {
            playerControllerEnabled = false;
            currentMouseDelta = Vector2.zero;
            currentMouseDeltaVelocity = Vector2.zero;
        }
        
        public virtual void EnablePlayerController() {
            playerControllerEnabled = true;
        }

        #region Settings

        // C O N T R O L L E R   S E T T I N G S
        private ControllerSettingsSO controllerSettings;

        private void UpdateControllerSettings() {
            controllerSettings = SettingsManager.ControllerSettings;
        }

        // P L A Y E R   S E T T I N G S

        public void UpdatePlayerSettings() {
            MouseSensitivity = SettingsManager.PlayerSettings.GetMouseSensitivity();
            mouseSmoothTime = SettingsManager.PlayerSettings.GetMouseSmoothTime();
            ControllerReferences.cam.fieldOfView = SettingsManager.PlayerSettings.GetCameraFov();
        }

        public static void ApplySettings(Vector2 _sensitivity) { MouseSensitivity = _sensitivity; }

        public static Vector2 GetSensitivity() { return MouseSensitivity; }

        #endregion
        
        public float GetPlayerSpeed(){ return moveSpeed; }
    }
}