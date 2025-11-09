using HouseTrap.Core.ScriptableVariables;
using UnityEngine;

namespace HouseTrap.BadThoughts {
    public class AbilitySO : ScriptableObject {
        public ScriptableVariableFloat cooldownTimer;
        public Sprite spriteImage;
        public bool triggerOnUpdate;

        public AudioClip activateSfx;

        public virtual void Initialise() {
        }

        public virtual void Disable() {
        }

        public virtual void Activate() {
        }

        public virtual void RunScript() {
        }
    }
}