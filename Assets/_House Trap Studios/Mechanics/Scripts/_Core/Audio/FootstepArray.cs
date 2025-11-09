
using HouseTrap.Core.EventSystem;
using UnityEngine;

namespace HouseTrap.Core.Audio {
    public class FootstepArray : SoundArray {
        [SerializeField] private GameEvent footstepEvent;

        public override void PlayRandom() {
            // if(ControllerReferences.characterController.isGrounded){
            footstepEvent.Raise();
            base.PlayRandom();
        }
    }
}