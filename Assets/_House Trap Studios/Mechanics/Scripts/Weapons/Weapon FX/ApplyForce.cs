using UnityEngine;

public class ApplyForce : MonoBehaviour
{
    [SerializeField] private float ForceMin;
    [SerializeField] private float ForceMax;
    [SerializeField] private Rigidbody rb;
    
    [SerializeField] private AudioClip sound;

    public float lifetime = 1f;
    public float _fadeTime = 2f;

    protected virtual void AddForce(){

    }
}
