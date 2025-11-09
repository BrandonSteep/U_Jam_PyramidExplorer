using UnityEngine;

public class LineFade : MonoBehaviour
{
    [SerializeField] private Color colour;
    [SerializeField] private float speed;

    LineRenderer line;

    private void Start()
    {
        line = GetComponent<LineRenderer>();
    }

    private void OnEnable()
    {
        colour.a = 50;
    }

    private void FixedUpdate()
    {
        // move towards 0
        colour.a = Mathf.Lerp(colour.a, 0, Time.deltaTime * speed);

        // update colour
        line.startColor = colour;
        line.endColor = colour;
    }
}
