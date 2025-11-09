using UnityEngine;

public class ToggleGameObjects : MonoBehaviour {
    [SerializeField] private GameObject[] objectsToToggle;

    public void Toggle() {
        foreach (var t in objectsToToggle)
        {
            t.SetActive(!t.activeInHierarchy);
        }
    }
}