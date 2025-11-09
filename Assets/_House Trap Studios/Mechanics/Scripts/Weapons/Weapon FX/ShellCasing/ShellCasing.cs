using System.Collections;
using UnityEngine;

public class ShellCasing : MonoBehaviour {
    [SerializeField] private float ForceMin = 0.1f;
    [SerializeField] private float ForceMax = 0.25f;
    [SerializeField] private float torque = 0.5f;
    [SerializeField] private Rigidbody rb;
    
    [SerializeField] private AudioClip casingSound;

    private float lifetime = 2f;
    private bool hasLanded;

    [SerializeField] private bool ejectsBackwards;
    
    // private float fadetime = 1f; // Used to Fade over time - Only works if Material is set to Fade

    private void OnEnable() {
        rb = GetComponent<Rigidbody>();

        if(rb == null){
            rb = this.gameObject.AddComponent<Rigidbody>();
            rb.mass = 0.1f;
        }

        AddForce();
    }

    private void AddForce() {
        var force = UnityEngine.Random.Range(ForceMin, ForceMax);
        if (!ejectsBackwards) {
            rb.AddForce((transform.right + new Vector3(0f, (force * 0.5f), 0f)) * force, ForceMode.Impulse);
        } else {
            rb.AddForce((transform.forward + new Vector3(0f, 0f, (force * 0.5f))) * -force, ForceMode.Impulse);       
        }

        rb.AddTorque(UnityEngine.Random.insideUnitSphere * (force * torque));

        StartCoroutine(Fade());
    }

    private IEnumerator Fade() {
        yield return new WaitForSeconds(lifetime);

        var fadePercent = 0f;
        var fadeTimer = 1f / lifetime;

        var fromScale = transform.localScale;

        while(fadePercent < 1f) {
            // Debug.Log(fadePercent);
            fadePercent += Time.deltaTime * fadeTimer;

            transform.localScale = Vector3.Lerp(fromScale, Vector3.zero, fadePercent);
            yield return null;
        }
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision _collision) {
        if (!_collision.collider.CompareTag("Ground") || hasLanded) return;
        hasLanded = true;
        this.gameObject.AddComponent<AudioSource>().PlayOneShot(casingSound);
        //GetComponent<Collider>().enabled = false;
        Destroy(rb, 1f);
    }
}
