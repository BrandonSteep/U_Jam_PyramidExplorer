using HouseTrap.Core;
using HouseTrap.Core.Controller;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSpawner : MonoBehaviour {
    public Transform spawnPoint;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Update(){
        Debug.Log($"Player Position = {this.transform.position}");
    }

    private void OnSceneLoaded(Scene _scene, LoadSceneMode _mode)
    {
        // Disable Character Controller;
        var cc = GetComponent<CharacterController>();
        var loco = GetComponent<PlayerControllerCc>();
        cc.enabled = false;
        loco.enabled = false;

        // Retrieve the next spawn point from player prefs
        Vector3 nextSpawnPosition = new Vector3(PlayerPrefs.GetFloat("NextPositionX", 0f), PlayerPrefs.GetFloat("NextPositionY", 0f), PlayerPrefs.GetFloat("NextPositionZ", 0f));
        Quaternion nextSpawnRotation = new Quaternion(PlayerPrefs.GetFloat("NextRotationX", 0f), PlayerPrefs.GetFloat("NextRotationY", 0f), PlayerPrefs.GetFloat("NextRotationZ", 0f), PlayerPrefs.GetFloat("NextRotationW", 1f));

        // Debug.Log($"New Spawn Point = {nextSpawnPosition}");

        // Use the next spawn point to set the player's spawn position
        if (nextSpawnPosition != Vector3.zero)
        {
            Debug.Log($"Spawning Player at {nextSpawnPosition}");

            gameObject.transform.position = nextSpawnPosition;
            gameObject.transform.rotation = nextSpawnRotation;
        }
        else if(spawnPoint != null){
            Debug.Log($"Next Spawn Position Missing - Spawning Player at {spawnPoint.position}");

            gameObject.transform.position = spawnPoint.position;
            gameObject.transform.rotation = spawnPoint.rotation;
        }
        else
        {
            Debug.LogWarning("No spawn point specified for player, defaulting to Set Scene Position");
            // this.gameObject.transform.position = new Vector3(0f, 1.15f, 0f);
        }

        // Reset the next spawn point to a default value
        PlayerPrefs.DeleteKey("NextPositionX");
        PlayerPrefs.DeleteKey("NextPositionY");
        PlayerPrefs.DeleteKey("NextPositionZ");
        PlayerPrefs.DeleteKey("NextRotationX");
        PlayerPrefs.DeleteKey("NextRotationY");
        PlayerPrefs.DeleteKey("NextRotationZ");

        // Enable Character Controller
        cc.enabled = true;
        loco.enabled = true;

        //Destroy this Component
        Destroy(this);
    }
}