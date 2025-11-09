using UnityEngine;

[CreateAssetMenu(fileName = "SpawnRef", menuName = "Shooting Range/Spawn Ref")]
public class SpawnRefSO : ScriptableObject
{
    [SerializeField] public GameObject obj;
    [SerializeField] public int spawnPointNum;
}
