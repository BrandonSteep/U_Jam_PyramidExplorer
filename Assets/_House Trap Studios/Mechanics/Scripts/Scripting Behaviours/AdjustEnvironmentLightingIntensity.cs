using System.Collections;
using UnityEngine;

public class AdjustEnvironmentLightingIntensity
{
    public IEnumerator ChangeAmbientLighting(float duration, float endValue, AnimationCurve curve){
        Debug.Log("Starting Lighting Change");

        float timeElapsed = 0f;
        float start = RenderSettings.ambientIntensity;

        while(timeElapsed < duration){
            float t = timeElapsed / duration;
            t = curve.Evaluate(t);

            RenderSettings.ambientIntensity = Mathf.Lerp(start, endValue, t);

            timeElapsed += Time.deltaTime;
            yield return null;
        }
        RenderSettings.ambientIntensity = endValue;
    }
}
