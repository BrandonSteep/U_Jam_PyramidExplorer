using UnityEngine;

namespace HouseTrap.Core.Interactions.Notes
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Note")]
    public class NoteSO : ScriptableObject {
        [SerializeField] private string noteName;
        [SerializeField] private Texture2D noteImage;

        [TextArea(3, 5)][SerializeField] private string[] noteContents;

        public string GetNoteName() {
            return noteName;
        }

        public Texture2D GetNoteImage() {
            return noteImage;
        }

        public string GetNoteContents(int _pageNumber) {
            return noteContents[_pageNumber];
        }

        public int GetPageLength() {
            return noteContents.Length;
        }
    }
}