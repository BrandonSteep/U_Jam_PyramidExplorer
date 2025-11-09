using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SmoothDampInterpolate_UI : MonoBehaviour
{
    [SerializeField] private AnimationCurve curve;
    [SerializeField] private Image image;

    void OnEnable(){
        image = GetComponent<Image>();
    }

    public void FlashSmall(){
        StartCoroutine(LerpValue(.25f, 0f, .35f));
    }

    public void FlashMid(){
        StartCoroutine(LerpValue(.75f, 0f, .5f));
    }

    public void FlashBig(){
        StartCoroutine(LerpValue(1f, 0f, 1f));
    }

    protected IEnumerator LerpValue(float start, float end, float duration){
        float timeElapsed = 0f;

        while(timeElapsed < duration){
            float t = timeElapsed / duration;
            t = curve.Evaluate(t);

            var newValue = Mathf.Lerp(start, end, t);

            image.color = new Color(1f, 1f, 1f, newValue);

            timeElapsed += Time.deltaTime;

            yield return null;
        }

        image.color = new Color(1f, 1f, 1f, 0f);
    }
}
