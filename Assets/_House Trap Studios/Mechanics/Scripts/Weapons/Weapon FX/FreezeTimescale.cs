using System.Collections;
using UnityEngine;

public class FreezeTimescale : MonoBehaviour
{
    [SerializeField]
    private float _pauseTime = 0.5f;

    // [HideInInspector]
    public bool _freezeTimescale = false;

    // Update is called once per frame
    void Update()
    {
        if(_freezeTimescale){
            StartCoroutine("PlayAfterSeconds");
            Time.timeScale = 0;
        }
    }

    private IEnumerator PlayAfterSeconds(){

        float pauseEndTime = Time.realtimeSinceStartup + _pauseTime;
        while (Time.realtimeSinceStartup < pauseEndTime)
        {   
            yield return null;
        }

        Time.timeScale = 1f;
        _freezeTimescale = false;
        yield return null;
    }
}
