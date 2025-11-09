using System.Collections;
using HouseTrap.Core.EventSystem;
using UnityEngine;

public class CanvasGroupFader : MonoBehaviour
{
    private CanvasGroup _canvas;
    [SerializeField] private GameEvent _fadeInEvent;
    [SerializeField] private GameEvent _fadeOutEvent;
    [SerializeField] private float _duration = 1f;

    void Awake(){
        _canvas = GetComponent<CanvasGroup>();
    }

    public void FadeIn(){
        StartCoroutine(PerformFade(0f, 1f, _fadeInEvent));
    }

    public void FadeOut(){
        StartCoroutine(PerformFade(1f, 0f, _fadeOutEvent));
    }

    private IEnumerator PerformFade(float oldValue, float newValue, GameEvent eventToCall){

        for (float t = 0f; t < _duration; t += Time.deltaTime) {
            _canvas.alpha = Mathf.Lerp(oldValue, newValue, t / _duration);
            yield return null;
        }
        _canvas.alpha = newValue;
        if(eventToCall != null){
            eventToCall.Raise();
        }
    }
}
