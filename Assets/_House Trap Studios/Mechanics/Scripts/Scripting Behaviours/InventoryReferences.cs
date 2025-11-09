using UnityEngine;

public class InventoryReferences : MonoBehaviour {
    public static ObjectPool ObjectPool;

    private void Start() {
        ObjectPool = GameObject.FindWithTag("ObjectPool").GetComponent<ObjectPool>();
    }
}
