using UnityEngine;
using HouseTrap.Audio;

public class Status : MonoBehaviour
{
    protected bool isAlive = true;
    protected bool canTakeDamage = true;

    [SerializeField] private AudioClip hitSound;
    [SerializeField] private float hitSoundCooldown = 0.05f;
    private bool canPlayHitSound = true;

    public bool CanTakeDamage() {
        return canTakeDamage;
    }

    protected virtual void Die() {
        isAlive = false;
        Destroy(this.gameObject);
    }

    public void AddIFrames(float _iFrames){
        canTakeDamage = false;
        if(_iFrames <= 0){
            Debug.Log($"iFrames not given - stat = {_iFrames}");
        }
        else{
            Debug.Log($"iFrames = {_iFrames}");

            canTakeDamage = false;
            Invoke(nameof(ResetDamage), _iFrames);
        }
    }

    public void ResetDamage() {
        Debug.Log("iFrames Over");
        canTakeDamage = true;
    }

    protected void PlayHitSound() {
        if (!canPlayHitSound) return;
        AudioManager.PlayOneShot(hitSound);
        canPlayHitSound = false;
        Invoke(nameof(ResetHitSound), hitSoundCooldown);
    }

    private void ResetHitSound(){
        canPlayHitSound = true;
    }
}