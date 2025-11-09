using System.Collections;
using HouseTrap.Core.EventSystem;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    [SerializeField] private AnimationCurve curve;
    [SerializeField] private float lerpTime;
    [SerializeField] private Vector3 rotationAmount = new Vector3(0f,45f,0f);
    [SerializeField] private GameEvent rotationComplete;
    private bool canActivate = true;
        
    public void RotateClockwise(){
        if(canActivate){
            StartCoroutine(LerpValue(lerpTime, true));
        }
    }

    public void Rotate_CounterClockwise(){
        if(canActivate){
            StartCoroutine(LerpValue(lerpTime, false));
        }
    }


    private IEnumerator LerpValue(float duration, bool clockwise){
        canActivate = false;

        float timeElapsed = 0f;
        Vector3 start = transform.eulerAngles;
        Quaternion end;
        
        if(!clockwise){
            end = Quaternion.Euler(transform.eulerAngles - rotationAmount);
        }
        else{
            end = Quaternion.Euler(transform.eulerAngles + rotationAmount);
        }

        while(timeElapsed < duration){
            float t = timeElapsed / duration;
            t = curve.Evaluate(t);
            
            transform.rotation = Quaternion.Lerp(Quaternion.Euler(start), end, t);

            timeElapsed += Time.deltaTime;

            yield return null;
        }
        if(!clockwise){
            transform.eulerAngles = start - rotationAmount;
        }
        else{
            transform.eulerAngles = start + rotationAmount;
        }
        rotationComplete.Raise();
        canActivate = true;
    }
}
