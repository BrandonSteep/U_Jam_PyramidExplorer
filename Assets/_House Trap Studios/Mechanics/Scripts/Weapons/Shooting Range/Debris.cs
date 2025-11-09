using System.Collections;
using UnityEngine;

public class Debris : MonoBehaviour {
    private Rigidbody rb;
    private Collider coll;

    private float lifetime = 5f;
    private float fadetime = 3f;

    private void OnEnable() {
        rb = GetComponent<Rigidbody>();
        Invoke(nameof(DisablePhysics), lifetime);
    }

    private void DisablePhysics() {
        Destroy(rb);
        Destroy(coll);

        StartCoroutine(Fade());
    }

    private IEnumerator Fade() {
        yield return new WaitForSeconds(lifetime);

        var percent = 0f;
        var fadeSpeed = 1f / fadetime;

        var rend = GetComponent<Renderer>();

        foreach (var mat in rend.materials) {
            Debug.Log(mat.name);
            var initialColour = mat.color;
            while (percent < 1f) {
                percent += Time.deltaTime * fadeSpeed;
                mat.color = Color.Lerp(initialColour, Color.clear, percent);
                yield return null;
            }
        }

        Destroy(gameObject);
    }
}