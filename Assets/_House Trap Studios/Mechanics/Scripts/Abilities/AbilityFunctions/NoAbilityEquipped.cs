using UnityEngine;

namespace HouseTrap.BadThoughts {
    [CreateAssetMenu(menuName = "Ability/No Ability")]
    public class NoAbilityEquipped : AbilitySO {
        public override void Activate() {
            Debug.Log("Nothing Happened");
        }
    }
}