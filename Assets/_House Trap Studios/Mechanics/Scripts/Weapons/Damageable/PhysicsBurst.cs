using UnityEngine;

public class PhysicsBurst : MonoBehaviour
{
    private Rigidbody[] rbs;
    private AudioSource aSource;

    void OnEnable(){
        rbs = GetComponentsInChildren<Rigidbody>();
        GetComponent<AudioSource>().pitch = UnityEngine.Random.Range(0.95f, 1.1f);
        Burst();
    }

    private void Burst(){
        foreach(Rigidbody i in rbs){
            i.AddExplosionForce(1f, this.transform.position, 0.5f, 0.25f, ForceMode.Impulse);
        }
    }
}
