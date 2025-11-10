using UnityEngine;

public class ObjectGroupSpawn : MonoBehaviour {
    private void OnEnable() {
        for (var i = transform.childCount-1; i > -1f ; i--) {
            transform.GetChild(i).parent = null;
        }

        Destroy(gameObject);
    }
}
