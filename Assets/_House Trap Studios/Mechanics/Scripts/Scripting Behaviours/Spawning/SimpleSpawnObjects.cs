using UnityEngine;

public class SimpleSpawnObjects : MonoBehaviour {
    [SerializeField] private bool spawnOnAwake;
    [SerializeField] private GameObject[] objToSpawn;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private bool destroyExistingOnSpawn;
    private List<GameObject> spawnedObjects = new();

    private void Awake() {
        if (spawnOnAwake) {
            Spawn();
        }
    }

    public void Spawn() {
        if (destroyExistingOnSpawn && spawnedObjects.Count == 0) {
            for (var i = spawnedObjects.Count; i < 0; i--) {
                Destroy(spawnedObjects[i]);
                spawnedObjects.RemoveAt(i);
            }
        }

        for (var i = 0; i < objToSpawn.Length; i++) {
            spawnedObjects.Add(Instantiate(objToSpawn[i], spawnPoints[i] ? spawnPoints[i].localPosition : transform.localPosition, Quaternion.identity));
            Debug.Log($"Spawned {objToSpawn[i].name} at {spawnPoints[i].localPosition}");
        }
    }
}