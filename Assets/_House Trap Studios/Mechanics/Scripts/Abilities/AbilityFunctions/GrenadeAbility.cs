using HouseTrap.Core;
using HouseTrap.Core.Controller;
using UnityEngine;

namespace HouseTrap.BadThoughts {
    [CreateAssetMenu(menuName = "Ability/Grenade")]
    public class GrenadeAbility : AbilitySO {
        [SerializeField] private GameObject projectile;

        public override void Activate() {
            var startPosition = ControllerReferences.cam.transform.position +
                                (ControllerReferences.cam.transform.forward * 1f);
            var startRotation = new Vector3(ControllerReferences.cam.transform.eulerAngles.x - 22.5f,
                ControllerReferences.player.transform.eulerAngles.y,
                ControllerReferences.cam.transform.eulerAngles.z);

            Debug.Log($"Throwing Grenade from {startRotation}");
            Instantiate(projectile, startPosition, Quaternion.Euler(startRotation));
        }
    }
}