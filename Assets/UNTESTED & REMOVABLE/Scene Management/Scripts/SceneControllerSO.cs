using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "Scene Controller")]
public class SceneControllerSO : ScriptableObject {
    [SerializeField] private string sceneName;
    private Transform playerSpawnTransform;

    public void LoadScene() { SceneManager.LoadScene(sceneName); }

    public void SpawnPlayer() {
        var player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) {
            Debug.LogError("No player object found in scene");
            return;
        }

        player.transform.position = playerSpawnTransform.position;
        player.transform.rotation = playerSpawnTransform.rotation;
    }

    public void SetPlayerSpawnTransform(Transform _playerSpawnTransform) {
        if (_playerSpawnTransform == null) {
            Debug.LogError("No player spawn transform set - Defaulting to 0,0,0");
            playerSpawnTransform.position = Vector3.zero;
            playerSpawnTransform.rotation = Quaternion.identity;
        } else {
            playerSpawnTransform = _playerSpawnTransform;
        }
    }
}