using UnityEngine;

public class SceneTransition : MonoBehaviour {
    [SerializeField] private SceneControllerSO sceneController;
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
            if (playerSpawnTransform != null) {
                Debug.Log($"Setting Next Position as {playerSpawnTransform.localPosition}");

                // Save Position to PlayerPrefs //
                PlayerPrefs.SetFloat("NextPositionX", playerSpawnTransform.localPosition.x);
                PlayerPrefs.SetFloat("NextPositionY", playerSpawnTransform.localPosition.y);
                PlayerPrefs.SetFloat("NextPositionZ", playerSpawnTransform.localPosition.z);

                // Save Rotation to PlayerPrefs //
                PlayerPrefs.SetFloat("NextRotationX", playerSpawnTransform.localRotation.x);
                PlayerPrefs.SetFloat("NextRotationY", playerSpawnTransform.localRotation.y);
                PlayerPrefs.SetFloat("NextRotationZ", playerSpawnTransform.localRotation.z);
                PlayerPrefs.SetFloat("NextRotationW", playerSpawnTransform.localRotation.w);
            }

            sceneController.LoadScene();
        }
    }
}