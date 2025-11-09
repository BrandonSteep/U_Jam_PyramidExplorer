using DG.Tweening;
using HouseTrap.Core.Controller;
using HouseTrap.Core.GameManagement;
using UnityEngine;

namespace HouseTrap.Core.Interactions.Inspection {
    public class InteractionInspectItem : InteractionInspect {
        // Control variables
        private Vector2 currentRotation = Vector2.zero;
        private Vector2 currentRotationVelocity = Vector2.zero;
        private Vector3 currentMovement = Vector3.zero;
        private Vector3 currentMovementVelocity = Vector3.zero;

        // Variables specific to interactable items that CAN be picked up
        private Vector3 minMovementClamp;
        private Vector3 maxMovementClamp;

        private Transform inspectionPoint;
        private Vector3 inspectionPointOriginalPosition;

        private Vector3 originalPosition;
        private Quaternion originalRotation;

        private void Start() {
            UpdateSettings();
            inspectionPoint = ControllerReferences.itemInspectPoint.transform;
            inspectionPointOriginalPosition = inspectionPoint.localPosition;

            originalPosition = this.transform.position;
            originalRotation = this.transform.rotation;
        }

        protected override void RunInspection() {
            // Debug.Log("Running Inspection");
            var cameraRight = ControllerReferences.cam.transform.right;
            var cameraUp = ControllerReferences.cam.transform.up;

            var localRight = transform.InverseTransformDirection(cameraRight);
            var localUp = transform.InverseTransformDirection(cameraUp);

            if (mouseRotation) {
                MouseRotation(localRight, localUp);
                WASDMovement();

            } else {
                WASDRotation(localRight, localUp);
                MouseMovement();
            }
        }

        #region Rotation

        private void MouseRotation(Vector3 _localRight, Vector3 _localUp) {
            Vector2 targetRotation = ControllerReferences.inputManager.GetMouseLookInput();
            // Debug.Log($"Mouse Rotation Target = {targetRotation}");

            currentRotation = Vector2.SmoothDamp(currentRotation, targetRotation, ref currentRotationVelocity,
                inspectionRotationSmoothTime);

            if (!invertedControls) {
                transform.Rotate(_localUp, -currentRotation.x * inspectionRotationSensitivity * Time.deltaTime,
                    Space.Self);
                transform.Rotate(_localRight, -currentRotation.y * inspectionRotationSensitivity * Time.deltaTime,
                    Space.Self);
            } else {
                transform.Rotate(_localUp, currentRotation.x * inspectionRotationSensitivity * Time.deltaTime,
                    Space.Self);
                transform.Rotate(_localRight, currentRotation.y * inspectionRotationSensitivity * Time.deltaTime,
                    Space.Self);
            }
        }

        private void WASDRotation(Vector3 _localRight, Vector3 _localUp) {
            Vector2 targetRotation = ControllerReferences.inputManager.GetHorizontalInput();
            // Debug.Log($"WASD Rotation Target = {targetRotation}");

            currentRotation = Vector2.SmoothDamp(currentRotation, targetRotation, ref currentRotationVelocity,
                inspectionRotationSmoothTime);

            if (!invertedControls) {
                transform.Rotate(_localUp, -currentRotation.x * inspectionRotationSensitivity * Time.deltaTime,
                    Space.Self);
                transform.Rotate(_localRight, -currentRotation.y * inspectionRotationSensitivity * Time.deltaTime,
                    Space.Self);
            } else {
                transform.Rotate(_localUp, currentRotation.x * inspectionRotationSensitivity * Time.deltaTime,
                    Space.Self);
                transform.Rotate(_localRight, currentRotation.y * inspectionRotationSensitivity * Time.deltaTime,
                    Space.Self);
            }
        }

        #endregion

        #region Movement

        private void WASDMovement() {
            var targetMovement2D = ControllerReferences.inputManager.GetHorizontalInput();
            var targetMovement = new Vector3(targetMovement2D.x, targetMovement2D.y,
                ControllerReferences.inputManager.GetScrolling());
            targetMovement.Normalize();
            // Debug.Log($"WASD Movement Target = {targetMovement}");

            currentMovement = Vector3.SmoothDamp(currentMovement, targetMovement, ref currentMovementVelocity,
                inspectionMoveSmoothTime);
            Vector3 velocity = currentMovement * inspectionMoveSensitivity;

            Vector3 newPosition = new Vector3(
                Mathf.Clamp(inspectionPoint.localPosition.x + velocity.x, minMovementClamp.x, maxMovementClamp.x),
                Mathf.Clamp(inspectionPoint.localPosition.y + velocity.y, minMovementClamp.y, maxMovementClamp.y),
                Mathf.Clamp(inspectionPoint.localPosition.z + velocity.z, minMovementClamp.z, maxMovementClamp.z));

            // Debug.Log($"New Position = {newPosition}");

            inspectionPoint.localPosition = newPosition;
        }

        private void MouseMovement() {
            var targetMovement2D = ControllerReferences.inputManager.GetMouseLookInput();
            var targetMovement = new Vector3(targetMovement2D.x, targetMovement2D.y,
                ControllerReferences.inputManager.GetScrolling());
            targetMovement.Normalize();
            // Debug.Log($"Mouse Movement Target = {targetMovement}");

            currentMovement = Vector3.SmoothDamp(currentMovement, targetMovement, ref currentMovementVelocity,
                inspectionMoveSmoothTime);
            var velocity = currentMovement * inspectionMoveSensitivity;

            var newPosition = new Vector3(
                Mathf.Clamp(inspectionPoint.localPosition.x + velocity.x, minMovementClamp.x, maxMovementClamp.x),
                Mathf.Clamp(inspectionPoint.localPosition.y + velocity.y, minMovementClamp.y, maxMovementClamp.y),
                Mathf.Clamp(inspectionPoint.localPosition.z + velocity.z, minMovementClamp.z, maxMovementClamp.z));

            // Debug.Log($"New Position = {newPosition}");

            inspectionPoint.localPosition = newPosition;
        }


        #endregion

        protected override void OnStartInspecting() {
            transform.parent = inspectionPoint;
            transform.DOMove(transform.parent.position, inspectTransitionTime);
        }

        protected override void OnStopInspecting() {
            DOTween.Kill(this.gameObject);
            transform.parent = null;
            transform.DOMove(originalPosition, inspectTransitionTime);
            transform.DORotateQuaternion(originalRotation, inspectTransitionTime);

            inspectionPoint.localPosition = inspectionPointOriginalPosition;
        }

        #region Settings

        private bool invertedControls;
        private bool mouseRotation;

        protected override void UpdateSettings() {
            invertedControls = SettingsManager.PlayerSettings.GetInspectionInvertedControls();
            mouseRotation = SettingsManager.PlayerSettings.GetInspectionMouseRotation();

            minMovementClamp = SettingsManager.InteractableSettings.GetMinMovementClamp();
            maxMovementClamp = SettingsManager.InteractableSettings.GetMaxMovementClamp();
            base.UpdateSettings();
        }

        #endregion
    }
}