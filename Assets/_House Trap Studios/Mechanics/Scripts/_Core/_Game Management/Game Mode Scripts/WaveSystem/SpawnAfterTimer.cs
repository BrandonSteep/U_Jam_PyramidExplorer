using UnityEngine;

namespace HouseTrap.BadThoughts {
    public class SpawnAfterTimer : SpawnGameObject {
        [SerializeField] private float spawnTimer;
        [SerializeField] private bool destroyOnSpawn = true;

        void OnEnable() {
            Invoke(nameof(Spawn), spawnTimer);
        }

        protected override void OnSpawn() {
            if (destroyOnSpawn) {
                Destroy(this.gameObject);
            }
        }
    }
}