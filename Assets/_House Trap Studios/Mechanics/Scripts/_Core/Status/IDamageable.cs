using UnityEngine;

public interface IDamageable{
    public void TakeDamage(float _damageAmount){}
    public void TakeDamage(RaycastHit _hit, float _damageAmount, GameObject _damageOrigin){}
    public void TakeDamage(Transform _other, float _damageAmount, float _forceAmount){}
    public void Die(){}
}
