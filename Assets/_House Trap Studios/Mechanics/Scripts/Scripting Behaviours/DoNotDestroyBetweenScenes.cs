using UnityEngine;

public class DoNotDestroyBetweenScenes : MonoBehaviour
{
    private void Awake(){
        DontDestroyOnLoad(this.gameObject);
    }
}
