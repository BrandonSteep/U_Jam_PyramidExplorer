using HouseTrap.Core.Interactions.Inspection;
using HouseTrap.Core.Interactions.Notes;
using UnityEngine;

namespace HouseTrap.Core.Interactions {
    public class InteractionReadNote : MonoBehaviour, IInteractable {
        [SerializeField] private GameObject noteScreen;
        [SerializeField] private NoteScreen noteScreenInstance;

        [SerializeField] private GameObject parent;
        [SerializeField] private InteractionInspectCollectableNote parentInteraction;

        // [SerializeField] private 

        private void Awake() {
            if (!parentInteraction) {
                parentInteraction = parent.GetComponent<InteractionInspectCollectableNote>();
            }
        }

        public void Interact() { OpenNote(); }

        private void OpenNote() {
            if (noteScreenInstance) return;
            Debug.Log("Opening Note");
            parentInteraction.PauseInspecting();
            noteScreenInstance = Instantiate(noteScreen).GetComponent<NoteScreen>();
            noteScreenInstance.SetNote(parentInteraction.GetNoteSO());
        }

        public void CloseNote() {
            if (noteScreenInstance != null) {
                Debug.Log("Closing Note");
                DisableNoteScreen();
                parentInteraction.ResumeInspecting();
            }
        }

        private void DisableNoteScreen() { Destroy(noteScreenInstance.gameObject); }
    }
}