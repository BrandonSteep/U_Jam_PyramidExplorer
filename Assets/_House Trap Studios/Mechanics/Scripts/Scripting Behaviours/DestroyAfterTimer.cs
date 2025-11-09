using System.Collections;
using UnityEngine;

public class DestroyAfterTimer : MonoBehaviour
{
    private DestroySelf _destroySelf;
    [SerializeField] private float _timeInSeconds;

    void OnEnable(){
        _destroySelf = GetComponent<DestroySelf>();
        StartCoroutine("CountdownTimer");
    }

    private IEnumerator CountdownTimer(){
        yield return new WaitForSeconds(_timeInSeconds);
        _destroySelf.RemoveFromHierarchy();
    }
}
