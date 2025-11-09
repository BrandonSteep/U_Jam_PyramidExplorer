using System.Collections;
using HouseTrap.Core;
using HouseTrap.Core.Controller;
using UnityEngine;

namespace HouseTrap.BadThoughts {
    public class ResourceSpawner : MonoBehaviour {
        [SerializeField] private float spawnTimer = 10f;
        [SerializeField] private float spawnChancePercentage = 20f;
        [SerializeField] private GameObject stimPack;
        [SerializeField] private GameObject medKit;
        [SerializeField] private Transform[] spawnPoints;
        [SerializeField] private List<GameObject> spawnedResources = new List<GameObject>();

        void Awake() {
            InvokeRepeating(nameof(SpawnMedsDecision), spawnTimer, spawnTimer);
        }

        public void OnWaveSpawn() {
            if (spawnedResources.Count <= 5) {
                SpawnMedsDecision();
            }
        }

        private void SpawnMedsDecision() {
            switch (ControllerReferences.playerStatus.currentHp.value) {
                case < 75f and > 35f: {
                    float randomValue = UnityEngine.Random.Range(0, 100);
                    if (randomValue <= spawnChancePercentage) {
                        SpawnItem(stimPack);
                    }
                    break;
                }
                case <= 35f: {
                    float randomValue = UnityEngine.Random.Range(0, 100);
                    if (randomValue <= spawnChancePercentage) {
                        SpawnItem(medKit);
                    }
                    break;
                }
            }
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private void SpawnItem(GameObject _itemToSpawn) {
            Debug.Log($"Spawning {gameObject.name}");

            var recentlySpawned = Instantiate(_itemToSpawn,
                ChooseSpawnPoint(UnityEngine.Random.Range(0, spawnPoints.Length)).position, Quaternion.identity);
            spawnedResources.Add(recentlySpawned);
            StartCoroutine(DestroyAfterTime(recentlySpawned));
        }

        private Transform ChooseSpawnPoint(int _spawnPointIndex) {
            var newSpawnPoint = spawnPoints[_spawnPointIndex];
            var hitColliders = Physics.OverlapSphere(newSpawnPoint.position, 1f);
            foreach (var other in hitColliders) {
                return other.CompareTag("Pickup") ? ChooseSpawnPoint(_spawnPointIndex + 1) : newSpawnPoint;
            }

            return newSpawnPoint;
        }

        private IEnumerator DestroyAfterTime(GameObject _goToDestroy) {
            yield return new WaitForSeconds(30f);
            spawnedResources.Remove(_goToDestroy);
            Destroy(_goToDestroy);
        }
    }
}