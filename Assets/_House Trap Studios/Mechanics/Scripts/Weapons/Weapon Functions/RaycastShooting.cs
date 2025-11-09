using System.Collections;
using UnityEngine;
using HouseTrap.Core;
using HouseTrap.Core.Controller;

namespace HouseTrap.BadThoughts {
    public class RaycastShooting : MonoBehaviour {
        [SerializeField] protected LayerMask hitLayers;
        [SerializeField] protected Transform muzzle;

        [SerializeField] private GameObject bloodSpatter;

        public virtual void Fire(GameObject _attackOrigin, WeaponStats _stats) {
            for (var i = _stats.GetShotCount(); i > 0; i--) {
                var shootDirection = ControllerReferences.cam.transform.forward + _stats.GetRandomSpread();
                shootDirection.Normalize();
                if (Physics.Raycast(ControllerReferences.cam.transform.position, shootDirection, out RaycastHit hit, float.MaxValue,
                        hitLayers)) {
                    if (_stats.PlaysTrail()) {
                        Trail(muzzle.position, ControllerReferences.cam.transform.forward + (shootDirection * hit.distance));
                    }

                    InventoryReferences.ObjectPool.SpawnFromPool("HitGeneric", hit.point,
                        Quaternion.LookRotation(hit.normal));

                    if (!hit.collider.CompareTag("Enemy") && !hit.collider.CompareTag("EnemySpawner")) continue;
                    hit.collider.GetComponent<IDamageable>().TakeDamage(hit, _stats.GetRandomDamage(), _attackOrigin);
                    Instantiate(bloodSpatter, hit.point, Quaternion.LookRotation(hit.normal));

                    // if(_pierceAmount > 0){
                    //     Ray ray = new Ray(Camera.main.transform.position + shootDirection * Mathf.Infinity, Camera.main.transform.forward);
                    //     Pierce(hit, ray);
                    // }
                } else {
                    if (_stats.PlaysTrail()) {
                        Trail(muzzle.position, ControllerReferences.cam.transform.forward + (shootDirection * 100));
                    }
                }
            }
        }

        protected virtual void Pierce(RaycastHit _firstHit, Ray _shootRay, WeaponStats _stats, GameObject _attackOrigin) {
            var lastHit = _firstHit;
            RaycastHit nextHit;
            for (var i = 0; i < _stats.GetPierceAmount(); i++) {


                if (Physics.Raycast(lastHit.point, _shootRay.direction, out nextHit, Mathf.Infinity)) {
                    if (_stats.PlaysTrail()) {
                        Trail(lastHit.point, nextHit.point);
                    }

                    InventoryReferences.ObjectPool.SpawnFromPool("HitGeneric", nextHit.point,
                        Quaternion.FromToRotation(Vector3.forward, nextHit.normal));

                    if (nextHit.collider.CompareTag("Enemy") || nextHit.collider.CompareTag("EnemySpawner")) {
                        nextHit.collider.GetComponent<IDamageable>()
                            .TakeDamage(nextHit, _stats.GetRandomDamage(), _attackOrigin);
                        Instantiate(bloodSpatter, nextHit.point, Quaternion.LookRotation(nextHit.normal));
                    }

                    lastHit = nextHit;
                }
                // else{
                //     if(_playTrail){
                //         Trail(lastHit.point, nextHit.point);
                //     }
                // }

            }
        }

        protected void Trail(Vector3 _spawnPosition, Vector3 _endPoint) {

            var bulletTrailEffect =
                InventoryReferences.ObjectPool.SpawnFromPool("BulletTrail", _spawnPosition, Quaternion.identity);

            var line = bulletTrailEffect.GetComponent<LineRenderer>();

            line.SetPosition(1, _endPoint);

            StartCoroutine(DestroyTrail(bulletTrailEffect));
        }

        private IEnumerator DestroyTrail(GameObject _trail) {
            yield return new WaitForSeconds(1f);
            _trail.SetActive(false);
        }
    }
}