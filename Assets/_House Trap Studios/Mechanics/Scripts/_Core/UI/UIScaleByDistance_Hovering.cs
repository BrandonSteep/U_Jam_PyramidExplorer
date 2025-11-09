using HouseTrap.Core.Controller;
using UnityEngine;

namespace HouseTrap.Core.UI {
    public class UIScaleByDistanceHovering : MonoBehaviour {
        [SerializeField] private float idealDistance = 2f;
        [SerializeField] private Vector3 idealScale;
        [SerializeField] private Vector3 minimumScale = Vector3.zero;

        private UIHoverAtWorldPosition hover;

        private void OnEnable() {
            if (!hover) {
                hover = GetComponent<UIHoverAtWorldPosition>();
            }
        }

        private void Update() { AdjustSize(); }

        private void AdjustSize() {
            var dist = Vector3.Distance(ControllerReferences.cam.transform.position,
                hover.GetHoverTransform().position);
            if (dist > idealDistance) {
                Vector3 scaleFactor = new Vector3(idealScale.x * (idealDistance / dist),
                    idealScale.y * (idealDistance / dist), 1f);
                transform.localScale = scaleFactor;
            } else {
                transform.localScale = idealScale;
            }

            if (transform.localScale.x <= minimumScale.x) {
                transform.localScale = minimumScale;
            }
        }
    }
}