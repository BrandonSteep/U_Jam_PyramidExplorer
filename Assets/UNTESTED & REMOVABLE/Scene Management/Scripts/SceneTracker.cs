using HouseTrap.Core.ScriptableVariables;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTracker : MonoBehaviour {
    [SerializeField] private ScriptableVariableInteger sceneIndex;

    private void OnEnable() { sceneIndex.value = SceneManager.GetActiveScene().buildIndex; }
}