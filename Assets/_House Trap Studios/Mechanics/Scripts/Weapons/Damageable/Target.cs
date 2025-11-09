using HouseTrap.Core.EventSystem;
using HouseTrap.Core.ScriptableVariables;
using UnityEngine;

public class Target : MonoBehaviour, IDamageable
{
    [Header("Points")]
    [SerializeField] private GameEvent score10;
    [SerializeField] private GameEvent score25;
    [SerializeField] private ScriptableVariableFloat totalScore;
    [SerializeField] private ScriptableVariableFloat highScore;
    [SerializeField] private bool bonusPoints;
    private bool canTakeDamage = true;

    [Header("Effects")]
    [SerializeField] private GameObject brokenTarget;
    private ParticleSystem particles;

    void OnEnable(){
        particles = GetComponentInChildren<ParticleSystem>();
    }

    public void TakeDamage(RaycastHit _hit, float _damageAmount) {
        if (!canTakeDamage) return;
        canTakeDamage = false;
        GetComponent<Collider>().enabled = false;
        Die();
    }

    public void Die(){
        var broken = Instantiate(brokenTarget, this.transform);
        broken.transform.SetParent(null);
        Destroy(gameObject);
    }

    private void IncreaseScore(){
        if(bonusPoints){
            score25.Raise();
        }
        else score10.Raise();
    }

    public void Despawn(){
        GetComponent<Collider>().enabled = false;
        GetComponent<Renderer>().enabled = false;

        particles.Play();

        Destroy(this.gameObject, 2f);
    }
}
