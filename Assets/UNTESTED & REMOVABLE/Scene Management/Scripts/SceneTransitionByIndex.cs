using HouseTrap.Core.ScriptableVariables;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionByIndex : MonoBehaviour {
    [SerializeField] private ScriptableVariableInteger sceneIndex;
    [SerializeField] private Transform playerSpawnTransform;

    private bool canTransition;

    private void OnTriggerEnter(Collider _other) {
        if (_other.CompareTag("Player")) {
            canTransition = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")) {
            canTransition = false;
        }
    }

    public void TriggerTransitionAfterTime(float time) { Invoke("TriggerTransition", time); }

    public void TriggerTransition() { canTransition = true; }

    private void Update() {
        if (canTransition) {
            SceneManager.LoadScene(sceneIndex.value);
        }
    }
}