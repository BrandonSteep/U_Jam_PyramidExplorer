using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject obj;
    [SerializeField] private Transform spawnPoint;

        protected void SpawnObject()
    {
        // Debug.Log("Eject");
        Instantiate(obj, spawnPoint.position, spawnPoint.rotation);
    }
}
