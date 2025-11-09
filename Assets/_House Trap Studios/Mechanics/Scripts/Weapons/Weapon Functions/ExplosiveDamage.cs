using UnityEngine;

public class ExplosiveDamage : MonoBehaviour
{
    [SerializeField] private ExplosiveStats stats;

    [SerializeField] private Collider coll;
    [SerializeField] private GameObject origin;
    [SerializeField] private LayerMask damageableLayers;

    void OnEnable(){
        coll = GetComponent<Collider>();

        Debug.Log($"Explosion Enabled - Ending in {stats.GetHitboxLingerTime()} seconds");

        Invoke("EndExplosion", stats.GetHitboxLingerTime());
    }

    private void OnTriggerEnter(Collider other){
        // Debug.Log($"Explosion hit {other.name}");
        if ((damageableLayers.value & (1 << other.transform.gameObject.layer)) > 0) {
            IDamageable damageable = other.GetComponent<IDamageable>();
            // Debug.Log($"Hit with Layermask. Status.canTakeDamage = {other.GetComponent<Status>().CanTakeDamage()}");
            if (damageable != null /* && other.GetComponent<Status>().CanTakeDamage()*/){
                // Debug.Log($"{other.name} is taking {stats.GetDamage()} points of Damage");
                other.GetComponent<IDamageable>().TakeDamage(this.transform, stats.GetDamage(), origin);
            }
        }
    }

    private void EndExplosion(){
        Destroy(coll);
        Destroy(this.gameObject, stats.GetLifetime());
    }
}
