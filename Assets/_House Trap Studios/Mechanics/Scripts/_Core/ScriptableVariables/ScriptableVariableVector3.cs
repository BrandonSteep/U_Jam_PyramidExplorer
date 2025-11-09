using UnityEngine;

namespace HouseTrap.Core.ScriptableVariables {
    [CreateAssetMenu(menuName = "Variable/Vector3")]
    public class ScriptableVariableVector3 : ScriptableObject {
        public Vector3 value;
    }
}