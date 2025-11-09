using HouseTrap.Core;
using HouseTrap.Core.Controller;
using UnityEngine;

namespace HouseTrap.BadThoughts {
    public class PlayerWeaponRaycastShooting : RaycastShooting {
        [SerializeField] private WeaponStats stats;
        public void Fire() { Fire(ControllerReferences.player, stats); }
    }
}