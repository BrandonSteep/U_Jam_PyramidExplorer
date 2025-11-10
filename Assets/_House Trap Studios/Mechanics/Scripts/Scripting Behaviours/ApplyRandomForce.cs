using UnityEngine;

public class ApplyRandomForce : MonoBehaviour {
    [SerializeField] private bool applyOnAwake;
    [SerializeField] private float forceMin;
    [SerializeField] private float forceMax;
    [SerializeField] private Rigidbody rb;

    private void Awake() {
        rb = GetComponent<Rigidbody>();
        
        if(!applyOnAwake) return;
        ApplyForce(GetRandomDirection());
    }

    private Vector3 GetRandomDirection() {
        return new Vector3(UnityEngine.Random.Range((float)-1, 1), 1, UnityEngine.Random.Range((float)-1, 1));
    }
    
    public void ApplyForce(Vector3 _forceDir) {
        Debug.Log($"Applying force in direction: {_forceDir}");
        rb.AddForce(_forceDir * UnityEngine.Random.Range(forceMin, forceMax), ForceMode.Impulse);
    }
}
