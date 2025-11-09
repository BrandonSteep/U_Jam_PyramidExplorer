using UnityEngine;

[CreateAssetMenu (menuName = "Weapons/ExplosiveStats")]
public class ExplosiveStats : ScriptableObject
{
    [SerializeField] private float damageMin = 120f;
    [SerializeField] private float damageMax = 180f;

    [SerializeField] private float hitboxLingerInSeconds = 0.25f;
    [SerializeField] private float totalLifetime = 3f;

    [SerializeField] private float projectileSpeed = 5f;

    public float GetDamage(){
        return UnityEngine.Random.Range(damageMin, damageMax);
    }

    public float GetHitboxLingerTime(){
        return hitboxLingerInSeconds;
    }

    public float GetLifetime(){
        return totalLifetime;
    }

    public float GetProjectileSpeed(){
        return projectileSpeed;
    }
}
