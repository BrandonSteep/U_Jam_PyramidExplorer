using UnityEngine;

public class SpawnShootingRangeFormation : MonoBehaviour
{
    [SerializeField] private SpawnRefSO[] spawnRef;
    [SerializeField] private Transform[] spawnPoint;


        public void Spawn(int i){
        Instantiate(spawnRef[i].obj, spawnPoint[spawnRef[i].spawnPointNum]);
    }
}
