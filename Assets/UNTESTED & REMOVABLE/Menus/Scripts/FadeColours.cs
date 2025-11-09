using System.Collections;
using HouseTrap.Core.EventSystem;
using TMPro;
using UnityEngine;

public class FadeColours : MonoBehaviour
{
    [SerializeField] private Color _colourA;
    [SerializeField] private Color _colourB;
    [SerializeField] private float _fadeDuration;
    [SerializeField] private GameEvent _fadeFinished;
    private TMP_Text _text;
    
    void Start()
    {
        _text = GetComponent<TMP_Text>();
    }
    
    public void FadeFromAToB(){
        StartCoroutine(PerformFade(_colourA, _colourB));
    }

    
    public void FadeFromBToA(){
        // Debug.Log($"Fading from {_colourA} to {_colourB}");
        StartCoroutine(PerformFade(_colourB, _colourA));
    }
    
    private IEnumerator PerformFade(Color A, Color B){

        for (float t = 0f; t < _fadeDuration; t += Time.deltaTime) {

            _text.color = Color.Lerp(A, B, t / _fadeDuration);
            yield return null;
        }
        
        if(_fadeFinished != null){
            _fadeFinished.Raise();
        }

        _text.color = B;
    }
}
