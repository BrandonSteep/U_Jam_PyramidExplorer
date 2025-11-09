using HouseTrap.Core;
using HouseTrap.Core.Controller;
using UnityEngine;

public class EquippedSway : MonoBehaviour
{
    // INPUT //
    private Vector2 mouseLookInput;
    private Vector3 initialPosition;

    // Parameters //
    public float amount = -0.1f;
    public float maxAmount = 0.1f;
    public float smoothAmount = 0.3f;

    private void Start() {
        initialPosition = transform.localPosition;
    }
    
    private void Update() {
        mouseLookInput = ControllerReferences.inputManager.GetMouseLookInput() * amount;
    }
    
    private void LateUpdate() {
        ApplySway();
    }

    private void ApplySway() {
        //Clamp the Sway//
        mouseLookInput.x = Mathf.Clamp(mouseLookInput.x, -maxAmount, maxAmount);
        mouseLookInput.y = Mathf.Clamp(mouseLookInput.y, -maxAmount, maxAmount);
        var finalPosition = new Vector3(mouseLookInput.x, mouseLookInput.y * 0.5f, 0);
        
        //Move the Object//
        transform.localPosition = Vector3.Lerp(transform.localPosition, finalPosition + initialPosition, smoothAmount * Time.deltaTime);
    }
}
