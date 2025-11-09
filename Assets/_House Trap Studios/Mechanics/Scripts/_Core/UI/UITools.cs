using HouseTrap.Core.Controller;
using UnityEngine;

namespace HouseTrap.Core {
    public class UITools {

        #region World Space to Canvas Space

        public Vector2 WorldSpaceToCanvasSpace(RectTransform _canvasRect, Transform _worldSpacePosition) {
            return WorldSpaceToCanvasSpace(_canvasRect, _worldSpacePosition.position);
        }

        public Vector2 WorldSpaceToCanvasSpace(RectTransform _canvasRect, Vector3 _worldSpacePosition) {
            Vector2 viewportPosition = ControllerReferences.cam.WorldToViewportPoint(_worldSpacePosition);

            var worldObjectScreenPosition = new Vector2(
                (viewportPosition.x * _canvasRect.sizeDelta.x) - (_canvasRect.sizeDelta.x * _canvasRect.pivot.x),
                (viewportPosition.y * _canvasRect.sizeDelta.y) - (_canvasRect.sizeDelta.y * _canvasRect.pivot.y));

            // Check if the UI Element is behind the Player Camera
            var dotProduct = Vector3.Dot(ControllerReferences.cam.transform.forward,
                _worldSpacePosition - ControllerReferences.cam.transform.position);
            // Debug.Log($"Dot = {dotProduct}");

            if (dotProduct < 0) {
                worldObjectScreenPosition = new Vector2(99999, 99999);
                // Debug.Log("The UI is behind me");
            }

            return worldObjectScreenPosition;
        }

        #endregion
    }
}