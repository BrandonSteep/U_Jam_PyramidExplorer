using HouseTrap.Core.ScriptableVariables;
using UnityEngine;

namespace HouseTrap.Core.GameManagement
{
    [CreateAssetMenu(menuName = "Settings SOs/Player Settings")]
    public class PlayerSettingsSO : ScriptableObject {
        #region First Person Settings

        [Header("First Person Player Settings")]
        [SerializeField] private ScriptableVariableFloat cameraFov;

        // [SerializeField] private Vector2 mouseSensitivity = new Vector2(35f, 35f);
        [SerializeField] private ScriptableVariableFloat mouseSensitivity;
        [SerializeField] private ScriptableVariableFloat mouseSmoothTime;

        public float GetCameraFov() { return cameraFov.value; }
        public Vector2 GetMouseSensitivity() { return new Vector2(mouseSensitivity.value, mouseSensitivity.value); }
        public float GetMouseSmoothTime() { return mouseSmoothTime.value; }

        #endregion

        #region Advanced Settings

        [Header("Item Inspection Settings")]
        [SerializeField]

        private bool inspectInvertedControls = true;
        [SerializeField] private bool inspectionMouseRotation;
        [SerializeField] [Range(0.1f, 0.5f)] private float inspectionLookSensitivity = 0.35f;
        [SerializeField] [Range(0.0f, 0.5f)] private float inspectionLooksSmoothTime = 0.3f;
        [SerializeField] [Range(0.25f, 1f)] private float inspectionRotationSensitivity = 0.5f;
        [SerializeField] [Range(0.0f, 0.5f)] private float inspectionRotationSmoothTime = 0.1f;
        [SerializeField] [Range(0.25f, 1f)] private float inspectionMovementSensitivity = 0.5f;
        [SerializeField] [Range(0.0f, 0.5f)] private float inspectionMovementSmoothTime = 0.1f;

        public bool GetInspectionInvertedControls() { return inspectInvertedControls; }
        public bool GetInspectionMouseRotation() { return inspectionMouseRotation; }
        public float GetInspectionLookSensitivity() { return inspectionLookSensitivity * 0.1f; }
        public float GetInspectionLookSmoothTime() { return inspectionLooksSmoothTime * 0.1f; }
        public float GetInspectionRotationSensitivity() { return inspectionRotationSensitivity * 250f; }
        public float GetInspectionRotationSmoothTime() { return inspectionRotationSmoothTime; }
        public float GetInspectionMovementSensitivity() { return inspectionMovementSensitivity * 0.01f; }
        public float GetInspectionMovementSmoothTime() { return inspectionMovementSmoothTime; }

        #endregion

        #region Update Settings
        public void UpdateSettings(float _fov, Vector2 _mouseSensitivity, float _mouseSmoothTime) {
            cameraFov.value = _fov;
            mouseSensitivity.value = _mouseSensitivity.x;
            mouseSmoothTime.value = _mouseSmoothTime;
        }

        #endregion
    }
}