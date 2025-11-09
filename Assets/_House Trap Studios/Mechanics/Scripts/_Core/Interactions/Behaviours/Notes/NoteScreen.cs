using HouseTrap.Core.Controller;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HouseTrap.Core.Interactions.Notes {
    public class NoteScreen : MonoBehaviour {
        [SerializeField] private NoteSO noteSO;

        [Header("Assigned UI")] [SerializeField]
        private TMP_Text uiNoteName;

        [SerializeField] private RawImage uiNoteImage;
        [SerializeField] private TMP_Text uiNoteContents;

        [SerializeField] private RawImage uiPreviousPageArrow;
        [SerializeField] private RawImage uiNextPageArrow;

        [Header("Stats")] [SerializeField] private int currentPage;
        [SerializeField] private bool canTurnPage = true;
        [SerializeField] private float turnPageTimer = 0.75f;

        private void Awake() {
            uiNoteName.text = noteSO.GetNoteName();
            uiNoteImage.texture = noteSO.GetNoteImage();
            SetNotePage(currentPage);
        }

        private void Update() {
            if (!canTurnPage) return;
            if (ControllerReferences.inputManager.GetHorizontalInput().x > 0.5f) {
                NextPage();
                Invoke(nameof(ResetPageTurning), turnPageTimer);
            }

            if (ControllerReferences.inputManager.GetHorizontalInput().x < -0.5f) {
                PreviousPage();
                Invoke(nameof(ResetPageTurning), turnPageTimer);
            }
        }

        public void SetNote(NoteSO _note) {
            if (noteSO) {
                // Override Default Note
                noteSO = _note;
            }

            uiNoteName.text = noteSO.GetNoteName();
            uiNoteImage.texture = noteSO.GetNoteImage();
            SetNotePage(currentPage);
        }

        private void SetNotePage(int _pageNumber) {
            Debug.Log($"Note Page Length = {noteSO.GetPageLength() - 1}");
            Debug.Log($"Page Number = {_pageNumber}");
            uiNoteContents.text = noteSO.GetNoteContents(_pageNumber);

            uiPreviousPageArrow.enabled = _pageNumber != 0;

            uiNextPageArrow.enabled = _pageNumber != noteSO.GetPageLength() - 1;
        }

        private void NextPage() {
            currentPage++;
            if (currentPage > noteSO.GetPageLength() - 1) {
                currentPage = noteSO.GetPageLength() - 1;
            } else {
                canTurnPage = false;
                SetNotePage(currentPage);
            }
        }

        private void PreviousPage() {
            currentPage--;
            if (currentPage < 0) {
                currentPage = 0;
            } else {
                canTurnPage = false;
                SetNotePage(currentPage);
            }
        }

        private void ResetPageTurning() { canTurnPage = true; }
    }
}