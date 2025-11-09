using UnityEngine;

public class LightFlickering : MonoBehaviour {
    private Light lightToFlicker;

    [SerializeField] private float minLightAmount = .66f;
    [SerializeField] private float maxLightAmount = 1.25f;
    [SerializeField] private float minLightSpeed = 0.025f;
    [SerializeField] private float maxLightSpeed = 0.5f;

    [SerializeField] private float minDarkAmount = .25f;
    [SerializeField] private float maxDarkAmount = .8f;
    [SerializeField] private float minDarkSpeed = 0.5f;
    [SerializeField] private float maxDarkSpeed = 1.5f;

    private void OnEnable(){
        lightToFlicker = GetComponent<Light>();
        Invoke(nameof(DisableLight), UnityEngine.Random.Range(minDarkSpeed, maxDarkSpeed));
    }

    private void EnableLight(){
        lightToFlicker.intensity = UnityEngine.Random.Range(minLightAmount, maxLightAmount);

        // light.enabled = true;
        Invoke(nameof(DisableLight), UnityEngine.Random.Range(minLightSpeed, maxLightSpeed));
    }
    private void DisableLight(){
        lightToFlicker.intensity = UnityEngine.Random.Range(minDarkAmount, maxDarkAmount);

        // light.enabled = false;
        Invoke(nameof(EnableLight), UnityEngine.Random.Range(minDarkSpeed, maxDarkSpeed));
    }
}
