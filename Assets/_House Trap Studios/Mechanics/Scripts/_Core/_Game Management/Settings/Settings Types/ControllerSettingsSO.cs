using HouseTrap.Core.ScriptableVariables;
using UnityEngine;

namespace HouseTrap.Core.GameManagement
{
    [CreateAssetMenu(menuName = "Settings SOs/Controller Settings")]
    public class ControllerSettingsSO : ScriptableObject {
        #region Variables
        [Header("Movement Speed")]
        [SerializeField][Tooltip("Default value: 10")] private float walkSpeedMultiplier = 5f;
        [SerializeField][Tooltip("Default value: 4")] private float airSpeedMultiplier = 2.0f;
        
        [Header("Jumping")]
        [SerializeField] private float jumpForce;
        [SerializeField] private bool betterJumpEnabled = true;
        [SerializeField] private float jumpCooldown = 0.15f;
        [SerializeField] private float fallMultiplier = 5f;
        [SerializeField] private float lowJumpMultiplier = 5f;
        
        [SerializeField] private float coyoteTime = 0.1f;
        [SerializeField] private float bufferTime = 0.1f;
        #endregion

        #region Return Functions
        public float GetWalkSpeedMultiplier() { return walkSpeedMultiplier; }
        public float GetJumpForce() { return jumpForce; }
        public bool GetBetterJumpEnabled() { return betterJumpEnabled; }
        public float GetJumpCooldown() { return jumpCooldown; }
        public float GetFallMultiplier() { return fallMultiplier; }
        public float GetLowJumpMultiplier() { return lowJumpMultiplier; }
        public float GetAirSpeedMultiplier() { return airSpeedMultiplier; }
        public float GetCoyoteTime() { return coyoteTime; }
        public float GetBufferTime() { return bufferTime; }
        #endregion
    }
}