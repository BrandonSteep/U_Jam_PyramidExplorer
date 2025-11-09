using DG.Tweening;
using HouseTrap.Core.Controller;
using UnityEngine;

namespace HouseTrap.Core.Interactions.Inspection {
    public class InteractionInspectObject : InteractionInspect {
        // Control variables
        private Vector2 currentMouseDelta = Vector2.zero;
        private Vector2 currentMouseDeltaVelocity = Vector2.zero;


        // Variables specific to interactable objects that CANNOT be picked up
        private Collider coll;
        private bool cameraReady;
        [SerializeField] private Transform cameraPoint;

        // Camera variables
        private Transform playerCamera;
        private Transform cameraHolder;
        private float cameraPitch;

        private Quaternion cameraRotationOriginal;
        // private Vector3 cameraHolderPositionOriginal;
        // private Quaternion cameraHolderRotationOriginal;

        private void Start() {
            UpdateSettings();
            coll = GetComponent<Collider>();
            playerCamera = ControllerReferences.cam.transform;
            cameraHolder = playerCamera.parent;
        }

        protected override void RunInspection() {
            Debug.Log($"Camera Ready = {cameraReady}");
            if (cameraReady) {
                UpdateMouseLook();
            }
        }

        // MOUSE LOOK //
        private void UpdateMouseLook() {
            var targetMouseDelta = ControllerReferences.inputManager.GetMouseLookInput();

            currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseDeltaVelocity,
                inspectionLooksSmoothTime);

            cameraPitch -= currentMouseDelta.y * (inspectionLookSensitivity * objectInspectionMultiplier);
            cameraPitch = Mathf.Clamp(cameraPitch, -65.0f, 65.0f);

            playerCamera.localEulerAngles = Vector3.right * cameraPitch;
            cameraHolder.Rotate(Vector3.up * (currentMouseDelta.x * (inspectionLookSensitivity * objectInspectionMultiplier)));
        }

        protected override void OnStartInspecting() {
            coll.enabled = false;
            // cameraHolderPositionOriginal = cameraHolder.position;

            cameraRotationOriginal = playerCamera.rotation;
            // cameraHolderRotationOriginal = cameraHolder.rotation;

            Vector3 moveToPosition = new Vector3(
                cameraPoint.position.x, cameraPoint.position.y - playerCamera.localPosition.y, cameraPoint.position.z);

            playerCamera.DOLocalRotate(Vector3.zero, inspectTransitionTime);

            cameraHolder.DOMove(moveToPosition, inspectTransitionTime);
            cameraHolder.DORotate(new Vector3(0f, cameraPoint.eulerAngles.y, 0f), inspectTransitionTime);

            cameraPitch = 0f;
            Invoke(nameof(SetCameraReady), inspectTransitionTime);
        }

        protected override void StopInspecting() {
            cameraReady = false;
            playerCamera.DORotateQuaternion(cameraRotationOriginal, inspectTransitionTime);

            cameraHolder.DOLocalMove(Vector3.zero, inspectTransitionTime);
            cameraHolder.DOLocalRotate(Vector3.zero, inspectTransitionTime);
            coll.enabled = true;
            base.StopInspecting();
        }

        private void SetCameraReady() {
            cameraReady = true;
        }
    }
}