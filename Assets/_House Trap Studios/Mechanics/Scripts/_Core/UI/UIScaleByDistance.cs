using UnityEngine;
using HouseTrap.Core;
using HouseTrap.Core.Controller;

namespace HouseTrap.Core.UI {
    public class UIScaleByDistance : MonoBehaviour {
        private RectTransform transformComponent;
        [SerializeField] private float idealDistance = 9f;
        [SerializeField] private Vector3 idealScale;

        private void OnEnable() { transformComponent = GetComponent<RectTransform>(); }

        private void Update() { AdjustSize(); }

        private void AdjustSize() {
            var dist = Vector3.Distance(ControllerReferences.cam.transform.position, transformComponent.position);
            var scaleFactor = new Vector3((idealScale.x / idealDistance) * dist,
                (idealScale.y / idealDistance) * dist, 1f);
            transformComponent.localScale = scaleFactor;
        }
    }
}