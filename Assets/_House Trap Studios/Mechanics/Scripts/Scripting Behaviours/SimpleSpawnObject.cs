using UnityEngine;

public class SimpleSpawnObject : MonoBehaviour
{
    [SerializeField] private GameObject _objToSpawn;

    public void Spawn(){
        Instantiate(_objToSpawn, this.transform);
    }
}
