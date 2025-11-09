using HouseTrap.Core.EventSystem;
using UnityEngine;

namespace HouseTrap.BadThoughts {
    public class WaveSpawner : MonoBehaviour {
        [SerializeField] private float waveTimer = 25f;
        [SerializeField] private float firstWaveDelay = 10f;
        [SerializeField] private bool randomSpawns;
        [SerializeField] private WaveSO[] wavesInOrder;
        [SerializeField] private WaveSO[] extraWaves;
        [SerializeField] private Transform[] spawnPoints;
        [SerializeField] private int waveNumber;

        [SerializeField] private bool spawnByDifficulty;
        [SerializeField] private int difficultyModifier;
        private int waveDifficulty;

        [SerializeField] private GameEvent waveEvent;

        void OnEnable() {
            if (randomSpawns) {
                InvokeRepeating(nameof(SpawnRandomWave), firstWaveDelay, 15f);
            } else {
                InvokeRepeating(nameof(SpawnWavesByCount), firstWaveDelay, waveTimer);
            }
        }

        private Transform ChooseSpawnPoint() {
            return spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)];
        }

        private void SpawnNewWave(WaveSO _waveToSpawn) {
            for (var i = 0; i < _waveToSpawn.GetWave().Length; i++) {
                var itemToSpawn = _waveToSpawn.GetWave()[i];
                Instantiate(itemToSpawn, ChooseSpawnPoint().position, Quaternion.identity);
            }
        }

        private void SpawnWavesByCount() {
            waveEvent.Raise();

            waveNumber++;
            // Debug.Log($"Wave Number = {waveNumber}");

            if (waveNumber < wavesInOrder.Length && !spawnByDifficulty) {
                SpawnNewWave(wavesInOrder[waveNumber]);
            } else {
                if (waveNumber >= wavesInOrder.Length) {
                    waveNumber = 0;
                }

                waveDifficulty = 0;
                spawnByDifficulty = true;

                SpawnNewWave(wavesInOrder[waveNumber]);
                waveDifficulty += wavesInOrder[waveNumber].GetDifficultyRating();
                difficultyModifier++;

                while (waveDifficulty < difficultyModifier) {
                    // Debug.Log($"Current Wave Difficulty = {waveDifficulty} & Current Difficulty = {difficultyModifier}");
                    SpawnWavesByDifficulty();
                }
            }
        }

        private void SpawnWavesByDifficulty() {
            var newWave = extraWaves[UnityEngine.Random.Range(0, wavesInOrder.Length)];
            SpawnNewWave(newWave);
            waveDifficulty += newWave.GetDifficultyRating();
        }

        private void SpawnRandomWave() {
            waveEvent.Raise();
            SpawnNewWave(wavesInOrder[UnityEngine.Random.Range(0, wavesInOrder.Length)]);
        }

        public Transform[] GetSpawnPoints() {
            return spawnPoints;
        }
    }
}