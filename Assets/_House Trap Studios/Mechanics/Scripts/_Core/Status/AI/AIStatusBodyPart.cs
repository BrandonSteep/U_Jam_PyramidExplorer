using HouseTrap.BadThoughts;
using UnityEngine;

public class AIStatusBodyPart : MonoBehaviour, IDamageable
{
    [SerializeField] private AIStatus mainStatus;
    
    public void TakeDamage(RaycastHit _hit, float _damageAmount, GameObject _damageOrigin){
        mainStatus.TakeDamage(_hit, _damageAmount, _damageOrigin);
    }
    public void TakeDamage(Transform _other, float _damageAmount, GameObject _damageOrigin){
        mainStatus.TakeDamage(_other, _damageAmount, _damageOrigin);
    }
}
