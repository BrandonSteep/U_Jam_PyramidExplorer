using HouseTrap.Core;
using HouseTrap.Core.Controller;
using UnityEngine;

public class BillboardSprites : MonoBehaviour {
    private void Update() {
        transform.LookAt(ControllerReferences.cam.transform.position);
        transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
    }
}
