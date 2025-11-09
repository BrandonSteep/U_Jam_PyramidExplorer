using UnityEngine;

public class AnimationOffset : MonoBehaviour {
    [SerializeField] private string animationToOffset;
    
    private void OnEnable()
    {
        GetComponent<Animator>().Play(animationToOffset, 0, UnityEngine.Random.Range(0f,1f));
    }
}
