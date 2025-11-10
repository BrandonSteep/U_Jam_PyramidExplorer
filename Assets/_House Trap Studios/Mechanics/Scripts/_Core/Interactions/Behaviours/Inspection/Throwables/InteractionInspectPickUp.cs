using HouseTrap.Core.Controller;
using HouseTrap.Core.GameManagement;
using UnityEngine;

namespace HouseTrap.Core.Interactions.Inspection.PhysicsObject {
    public class InteractionInspectPickUp : InteractionInspect {
        // Physics variables
        private Rigidbody rb;
        private Quaternion relativeRotation;
        [SerializeField] private float moveForceMultiplier = 15f;
        [SerializeField] private float rotationSpeed = 10f;
        private float maxDistance;

        [SerializeField] private float throwRotationOffset = 22.5f;
        
        // Control variables
        private Vector2 currentRotation, currentRotationVelocity;

        private Transform inspectionPoint;

        private Vector3 originalPosition;
        private Quaternion originalRotation;

        private void Start() {
            UpdateSettings();
            inspectionPoint = ControllerReferences.itemInspectPoint.transform;
            
            // Move over to Object Physics Handler later
            rb = GetComponent<Rigidbody>();
        }

        protected override void RunInspection() {
            MoveObject();
            // if (mouseRotation) {
            //     MouseRotation();
            // }
        }

        #region Object Movement

        private void MoveObject() {
            ObjectPosition();
            ObjectRotation();
            CheckObjectDistance();
        }

        private void ObjectPosition() {
            var targetPos = ControllerReferences.itemInspectPoint.transform.position;
            rb.linearVelocity = (targetPos - rb.position) * moveForceMultiplier;
        }

        private void ObjectRotation() {
            if (!ControllerReferences.player) return;
            
            var directionToPlayer = ControllerReferences.player.transform.position - transform.position;
            if (!(directionToPlayer.sqrMagnitude > 0.001f)) return;
            
            var targetRotation = Quaternion.LookRotation(directionToPlayer);
            // rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, Time.fixedDeltaTime * rotationSpeed));
            
            var deltaRotation = targetRotation * Quaternion.Inverse(rb.rotation);
            
            deltaRotation.ToAngleAxis(out var angle, out var axis);
            
            if (angle > 180f) angle -= 360f;
            
            var targetAngularVelocity = axis.normalized * (angle * Mathf.Deg2Rad * rotationSpeed);
            rb.angularVelocity = Vector3.Lerp(rb.angularVelocity, targetAngularVelocity, Time.fixedDeltaTime * 10f);
        }

        private void CheckObjectDistance() {
            if (Vector3.Distance(transform.position, inspectionPoint.transform.position) > maxDistance) {
                StopInspecting();
            }
        }

        #endregion
        
        public void ThrowObject() {
            if (!isInspecting) return;
            StopInspecting();
            var throwAngle =  Quaternion.AngleAxis(-throwRotationOffset, ControllerReferences.cam.transform.right) * ControllerReferences.cam.transform.forward;
            rb.AddForce(throwAngle * SettingsManager.ControllerSettings.GetThrowForce(), ForceMode.Impulse);
        }
        
        #region Rotation

        // private void MouseRotation() {
        //     var targetRotation = ControllerReferences.inputManager.GetMouseLookInput();
        //     
        //     var cameraRight = ControllerReferences.cam.transform.right;
        //     var cameraUp = ControllerReferences.cam.transform.up;
        //
        //     var localRight = transform.InverseTransformDirection(cameraRight);
        //     var localUp = transform.InverseTransformDirection(cameraUp);
        //
        //     currentRotation = Vector2.SmoothDamp(currentRotation, targetRotation, ref currentRotationVelocity,
        //         inspectionRotationSmoothTime);
        //
        //     if (!invertedControls) {
        //         transform.Rotate(localUp, -currentRotation.x * inspectionRotationSensitivity * Time.deltaTime,
        //             Space.Self);
        //         transform.Rotate(localRight, -currentRotation.y * inspectionRotationSensitivity * Time.deltaTime,
        //             Space.Self);
        //     } else {
        //         transform.Rotate(localUp, currentRotation.x * inspectionRotationSensitivity * Time.deltaTime,
        //             Space.Self);
        //         transform.Rotate(localRight, currentRotation.y * inspectionRotationSensitivity * Time.deltaTime,
        //             Space.Self);
        //     }
        // }

        #endregion
        
        protected override void OnStartInspecting() {
            ControllerReferences.interactionRaycast.DisableInteraction();
            transform.parent = ControllerReferences.itemInspectPoint.transform;
            relativeRotation = transform.localRotation;
            transform.parent = null;
            Debug.Log($"Relative Rotation = {relativeRotation.eulerAngles}");
        }

        protected override void OnStopInspecting() {
            ControllerReferences.interactionRaycast.EnableInteraction();
        }

        #region Settings

        private bool invertedControls;
        private bool mouseRotation;

        protected override void UpdateSettings() {
            invertedControls = SettingsManager.PlayerSettings.GetInspectionInvertedControls();
            mouseRotation = SettingsManager.PlayerSettings.GetInspectionMouseRotation();
            maxDistance = SettingsManager.ControllerSettings.GetInteractionDistance() + 0.25f;

            base.UpdateSettings();
        }

        #endregion
    }
}