using UnityEngine;

namespace HouseTrap.Core.GameManagement
{
    [CreateAssetMenu(menuName = "Settings SOs/Interactable Settings")]
    public class InteractableSettingsSO : ScriptableObject {
        [SerializeField] private Vector3 minMovementClamp = new Vector3(-0.5f, -0.3f, 0.5f);
        [SerializeField] private Vector3 maxMovementClamp = new Vector3(0.5f, 0.3f, 1f);
        [SerializeField] private float inspectTransitionTime = 0.5f;
        [SerializeField] private float objectInspectionMultiplier = 2f;

        #region Return Functions
        public Vector3 GetMinMovementClamp() { return minMovementClamp; }
        public Vector3 GetMaxMovementClamp() { return maxMovementClamp; }
        public float GetInspectTransitionTime() { return inspectTransitionTime; }
        public float GetObjectInspectionMultiplier() { return objectInspectionMultiplier; }
        #endregion
    }
}