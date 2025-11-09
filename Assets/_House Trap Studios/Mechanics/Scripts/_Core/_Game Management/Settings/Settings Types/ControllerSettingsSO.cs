using HouseTrap.Core.ScriptableVariables;
using UnityEngine;

namespace HouseTrap.Core.GameManagement
{
    [CreateAssetMenu(menuName = "Settings SOs/Controller Settings")]
    public class ControllerSettingsSO : ScriptableObject {
        #region Variables
        [Header("Walking")]
        [SerializeField][Tooltip("Default: Default 2.75")] private float walkSpeed = 2.75f;
        [SerializeField][Tooltip("Default: Default 0.75")] private float aimWalkSpeedMultiplier = 0.75f;
        [SerializeField][Range(0.0f, 0.5f)] private float moveSmoothTime = 0.15f;

        [Header("Running")]
        [SerializeField] private float runSpeedMultiplier = 2.0f;
        [SerializeField][Tooltip("Default: 100")] private ScriptableVariableFloat currentStamina;
        [SerializeField][Tooltip("Default: 100")] private ScriptableVariableFloat maxStamina;
        [SerializeField][Tooltip("Default: 15")] private float staminaDrainRate = 20f;
        [SerializeField][Tooltip("Default: 20")] private float staminaRefillRate = 20f;

        [Header("Slope Handling")]
        [SerializeField] private float slopeForce = 100f;
        [SerializeField] private float slopeForceRayLength;

        [Header("Misc")]
        [SerializeField] private float gravity = -13.0f;
        [SerializeField] private bool lockCursor = true;
        #endregion

        #region Return Functions
        public float GetWalkSpeed() { return walkSpeed; }
        public float GetAimWalkSpeedMultiplier() { return aimWalkSpeedMultiplier; }
        public float GetMoveSmoothTime() { return moveSmoothTime; }
        public float GetRunSpeedMultiplier() { return runSpeedMultiplier; }
        public ScriptableVariableFloat GetCurrentStaminaSO() { return currentStamina; }
        public ScriptableVariableFloat GetMaxStaminaSO() { return currentStamina; }
        public float GetStaminaDrainRate() { return staminaDrainRate; }
        public float GetStaminaRefillRate() { return staminaRefillRate; }
        public float GetSlopeForce() { return slopeForce; }
        public float GetSlopeForceRayLength() { return slopeForceRayLength; }
        public float GetGravity() { return gravity; }
        public bool GetLockCursor() { return lockCursor; }
        #endregion
    }
}