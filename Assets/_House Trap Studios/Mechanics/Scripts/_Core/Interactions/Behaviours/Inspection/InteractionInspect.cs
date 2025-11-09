using HouseTrap.Core.Controller;
using HouseTrap.Core.EventSystem;
using HouseTrap.Core.GameManagement;
using UnityEngine;

namespace HouseTrap.Core.Interactions.Inspection {
    public class InteractionInspect : PausePlayerController, IInteractable {
        #region Variables

        // TEMP STATS UNTIL WE GET ITEMS IN THE GAME //
        [SerializeField] protected string itemName;
        [SerializeField] protected GameObject itemNameTextBoxPrefab;

        [SerializeField] protected GameObject textBoxPrefab;
        protected UI.UITextFade itemNameinstance;

        protected GameEventListener escapeEvent;
        protected bool isInspecting;

        #endregion

        private void OnEnable() {
            escapeEvent = GetComponent<GameEventListener>();
        }

        public void Interact() {
            if (!isInspecting) {
                StartInspecting();
            } else {
                TextBoxManager.CreateNewHoveringTextBox_TypewriterFX("Nothing out of the ordinary", textBoxPrefab,
                    this.transform, 15f);
                // StopInspecting();
            }
        }

        #region Run Inspection

        void FixedUpdate() {
            if (isInspecting) {
                RunInspection();
            }
        }

        protected virtual void RunInspection() {
            Debug.Log("Running Default Inspection Script");
        }

        #endregion

        #region Enable & Disable Inspection

        protected virtual void StartInspecting() {
            itemNameinstance = TextBoxManager.CreateNewStaticTextBox_TypewriterFX(itemName, itemNameTextBoxPrefab, 30f)
                .GetComponent<UI.UITextFade>();

            isInspecting = true;
            if (pausePlayerEvent != null) {
                PausePlayer();
            } else {
                Debug.LogWarning($"Pause Player Event not set on {this.gameObject.name}");
            }

            OnStartInspecting();
        }

        protected virtual void OnStartInspecting() {
        }

        protected virtual void StopInspecting() {
            ControllerReferences.interactionRaycast.DisableInteraction();
            isInspecting = false;
            if (resumePlayerEvent != null) {
                Invoke("ResumePlayer", inspectTransitionTime);
            } else {
                Debug.LogWarning($"Resume Player Event not set on {this.gameObject.name}");
            }

            TextBoxManager.Instance.ClosePopups();
            itemNameinstance.Fade();
            OnStopInspecting();
        }

        protected virtual void OnStopInspecting() {
        }

        public virtual void PauseInspecting() {
            isInspecting = false;
            TextBoxManager.Instance.ClosePopups();
            itemNameinstance.Fade();
        }

        public virtual void ResumeInspecting() {
            itemNameinstance = TextBoxManager.CreateNewStaticTextBox_TypewriterFX(itemName, itemNameTextBoxPrefab, 30f)
                .GetComponent<UI.UITextFade>();
            Invoke(nameof(SetInspectingTrue), 0.25f);
        }

        public void CallStopInspecting() {
            if (isInspecting) {
                StopInspecting();
            }
        }

        private void SetInspectingTrue() {
            isInspecting = true;
        }

        #endregion

        #region Settings

        protected float inspectionLookSensitivity;
        protected float inspectionLooksSmoothTime;
        protected float inspectionMoveSensitivity;
        protected float inspectionMoveSmoothTime;
        protected float inspectionRotationSensitivity;
        protected float inspectionRotationSmoothTime;
        protected float inspectTransitionTime;
        protected float objectInspectionMultiplier;

        protected virtual void UpdateSettings() {
            inspectionLookSensitivity = SettingsManager.PlayerSettings.GetInspectionLookSensitivity();
            inspectionLooksSmoothTime = SettingsManager.PlayerSettings.GetInspectionLookSmoothTime();
            inspectionMoveSensitivity = SettingsManager.PlayerSettings.GetInspectionMovementSensitivity();
            inspectionMoveSmoothTime = SettingsManager.PlayerSettings.GetInspectionMovementSmoothTime();
            inspectionRotationSensitivity = SettingsManager.PlayerSettings.GetInspectionRotationSensitivity();
            inspectionRotationSmoothTime = SettingsManager.PlayerSettings.GetInspectionRotationSmoothTime();
            inspectTransitionTime = SettingsManager.InteractableSettings.GetInspectTransitionTime();
            objectInspectionMultiplier = SettingsManager.InteractableSettings.GetObjectInspectionMultiplier();
        }

        #endregion
    }
}