using UnityEngine;

namespace HouseTrap.Core.Interactions.Pickups {
    public class DropItem : MonoBehaviour {
        [SerializeField] private GameObject itemDrop;
        [SerializeField] private Transform spawnPoint;

        public void SpawnItem() {
            Instantiate(itemDrop, spawnPoint.position, Quaternion.identity);
        }
    }
}