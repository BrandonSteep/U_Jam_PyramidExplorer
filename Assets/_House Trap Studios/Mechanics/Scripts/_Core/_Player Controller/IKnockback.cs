using UnityEngine;

namespace HouseTrap.Core {
    public interface IKnockback {
        public void AddImpact(Vector3 _direction, float _forceAmount, ForceMode _forceMode);
        public void AddImpact(Transform _other, float _forceAmount);
    }
}