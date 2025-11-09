using DG.Tweening;
using UnityEngine;

namespace HouseTrap.Core.Interactions.Pickups {
    public class ItemSpin : MonoBehaviour {
        [SerializeField] private float duration = 1.5f;
        [SerializeField] private Vector3 endRotation;

        private void Awake() {
            transform.DORotate(endRotation, duration, RotateMode.FastBeyond360).SetEase(Ease.InOutSine)
                .SetLoops(-1, LoopType.Restart);
        }
    }
}