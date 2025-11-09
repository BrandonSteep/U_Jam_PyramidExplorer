using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

#if (UNITY_EDITOR)
public class EditorShortCutKeys : ScriptableObject {
    [MenuItem("Edit/Run _F5")] // shortcut key F5 to Play (and exit playmode also)
    private static void PlayGame() {
        if (!Application.isPlaying) {
            EditorSceneManager.SaveScene(SceneManager.GetActiveScene(), "", false);
            EditorApplication.EnterPlaymode();
        } else {
            EditorApplication.ExitPlaymode();
        }
    }
}
#endif