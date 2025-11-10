using UnityEngine;


public class ItemSpawner : MonoBehaviour {
    [SerializeField] private GameObject[] itemDrops;
    [SerializeField] private float dropChance = 0.5f;

    public void SpawnItem() {
        if (UnityEngine.Random.Range(0f, 1f) < dropChance)
            Instantiate(itemDrops[UnityEngine.Random.Range(0, itemDrops.Length)], transform.position, Quaternion.identity);
    }
}
