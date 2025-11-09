using UnityEngine;

namespace HouseTrap.BadThoughts {
    [CreateAssetMenu(menuName = "Ability/~Currently Active~")]
    public class CurrentlyActiveAbility : ScriptableObject {
        public AbilitySO activeAbility;
    }
}