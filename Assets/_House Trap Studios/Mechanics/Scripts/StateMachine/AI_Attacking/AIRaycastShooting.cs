using UnityEngine;

namespace HouseTrap.BadThoughts {
    public class AIRaycastShooting : RaycastShooting {
        [SerializeField] private ParticleSystem particles;

        public void FireFromMuzzle(GameObject _attackOrigin, WeaponStats _stats) {
            for (var i = _stats.GetShotCount(); i > 0; i--) {
                var shootDirection = -muzzle.transform.forward + _stats.GetRandomSpread();
                shootDirection.Normalize();
                if (Physics.Raycast(muzzle.transform.position, shootDirection, out RaycastHit hit, float.MaxValue,
                        hitLayers)) {
                    if (_stats.PlaysTrail()) {
                        Trail(muzzle.position, -muzzle.transform.forward + (shootDirection * hit.distance));
                    }

                    InventoryReferences.ObjectPool.SpawnFromPool("HitGeneric", hit.point,
                        Quaternion.LookRotation(hit.normal));

                    if (!hit.collider.CompareTag("Player") && !hit.collider.CompareTag("Enemy") && !hit.collider.CompareTag("EnemySpawner")) continue;
                    hit.collider.GetComponent<IDamageable>().TakeDamage(hit, _stats.GetRandomDamage(), _attackOrigin);
                } else {
                    if (_stats.PlaysTrail()) {
                        Trail(muzzle.position, -muzzle.transform.forward + (shootDirection * 100));
                    }

                    if (particles != null) {
                        particles.Play();
                    }
                }
            }
        }
    }
}