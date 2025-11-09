using System.Collections;
using UnityEngine;

public class SmoothDampInterpolate : MonoBehaviour
{
    [SerializeField] private AnimationCurve curve;

    public virtual void BeginInterpolation(Vector3 end, float duration){
        StartCoroutine(LerpValue(transform.position, end, duration));
    }

    protected IEnumerator LerpValue(Vector3 start, Vector3 end, float duration){
        float timeElapsed = 0f;

        while(timeElapsed < duration){
            float t = timeElapsed / duration;
            t = curve.Evaluate(t);

            var newPos = Vector3.Lerp(start, end, t);

            // Debug.Log($"{newPos}");
            transform.position = newPos;

            timeElapsed += Time.deltaTime;

            yield return null;
        }

        transform.position = end;
    }
}
