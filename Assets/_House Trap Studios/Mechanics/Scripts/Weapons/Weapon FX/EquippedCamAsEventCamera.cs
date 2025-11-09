using HouseTrap.Core;
using HouseTrap.Core.Controller;
using UnityEngine;

public class EquippedCamAsEventCamera : MonoBehaviour {
    [SerializeField] private Canvas sliderCanvas;

    private void OnEnable() { sliderCanvas.worldCamera = ControllerReferences.cam.transform.GetChild(0).GetComponent<Camera>(); }
}