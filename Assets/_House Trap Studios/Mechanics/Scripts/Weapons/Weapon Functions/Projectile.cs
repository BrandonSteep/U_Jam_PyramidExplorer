using System.Collections;
using UnityEngine;

namespace HouseTrap.BadThoughts {
    public class Projectile : MonoBehaviour {
        private GameObject origin;
        [SerializeField] private AIStats stats;
        [SerializeField] private ExplosiveStats explosiveStats;

        [SerializeField] private bool destroyOnContact = true;

        [SerializeField] private Rigidbody rb;
        [SerializeField] private LayerMask shootableLayers;

        [SerializeField] private GameObject splashParticle;

        // AOE Settings
        [SerializeField] private bool aoe;
        [SerializeField] private GameObject aoeHit;

        // // Effects
        // [SerializeField] private AudioClip hitSound;

        private void OnEnable() {
            rb = GetComponent<Rigidbody>();
            var forward = transform.forward;
            if (!aoe) {
                rb.AddForce(-forward * stats.GetProjectileSpeed(), ForceMode.Impulse);
            } else {
                rb.AddForce(forward * explosiveStats.GetProjectileSpeed(), ForceMode.Impulse);
            }

            StartCoroutine(nameof(Timer));
        }

        public void ProjectileSetup(GameObject _originObj) {
            origin = _originObj;
        }

        private void OnTriggerEnter(Collider _other) {
            // Debug.Log($"Projectile Collided with {other.gameObject.name}");
            if (!destroyOnContact) return;
            if ((shootableLayers.value & (1 << _other.gameObject.layer)) == 0) return;
            if (_other.CompareTag("Player") && stats != null || _other.CompareTag("Enemy") && stats != null) {
                _other.GetComponent<IDamageable>()
                    .TakeDamage(this.transform, stats.GetProjectileDamage(), 5f);
            }

            DestroyProjectile();
        }

        public void DestroyProjectile() {
            // Instantiate(splashParticle, this.transform.position, Quaternion.identity);
            if (aoe) {
                Instantiate(aoeHit, this.transform.position, Quaternion.identity);
            }

            Destroy(gameObject);
        }

        private IEnumerator Timer() {
            if (explosiveStats) {
                yield return new WaitForSeconds(explosiveStats.GetLifetime());
            } else {
                yield return new WaitForSeconds(stats.GetProjectileLifetime());
            }

            DestroyProjectile();
            yield return null;
        }
    }
}