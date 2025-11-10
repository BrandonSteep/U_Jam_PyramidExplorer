using UnityEngine;

namespace HouseTrap.Core {
    public class UIHoverAtWorldPosition : MonoBehaviour {
        private readonly UITools tools = new UITools();
        [SerializeField] private Transform hoverTransform;
        [SerializeField] private Vector3 hoverOffset = Vector3.zero;

        [SerializeField] private RectTransform uiElement;
        [SerializeField] private RectTransform uiCanvasRectTransform;

        private void OnEnable() {
            SetHoverTransform(this.transform);
            uiElement = this.transform.GetComponent<RectTransform>();

            if (!uiCanvasRectTransform) {
                uiCanvasRectTransform = TextBoxManager.Instance.GetComponent<RectTransform>();
            }
        }

        private void Update() {
            if (!hoverTransform) return;
            uiElement.anchoredPosition = hoverOffset != Vector3.zero ? tools.WorldSpaceToCanvasSpace(
                uiCanvasRectTransform, hoverTransform.position + hoverOffset) : tools.WorldSpaceToCanvasSpace(uiCanvasRectTransform, hoverTransform);
        }

        public void SetHoverTransform(Transform _newHoverTransform) {
            hoverTransform = _newHoverTransform;
        }

        public Transform GetHoverTransform() {
            return !hoverTransform ? transform : hoverTransform;
        }
    }
}