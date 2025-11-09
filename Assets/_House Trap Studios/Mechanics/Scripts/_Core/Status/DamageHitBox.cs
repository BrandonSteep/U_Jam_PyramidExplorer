using HouseTrap.Core.Controller;
using UnityEngine;

namespace HouseTrap.Core {
    public class DamageHitBox : MonoBehaviour {
        [SerializeField] private WeaponStats stats;

        private void OnTriggerEnter(Collider _other) {
            if (_other.gameObject == ControllerReferences.player) {
                ControllerReferences.playerStatus.TakeDamage(this.transform,
                    UnityEngine.Random.Range(stats.GetDamageMin(), stats.GetDamageMax()), this.gameObject);
            }
        }
    }
}