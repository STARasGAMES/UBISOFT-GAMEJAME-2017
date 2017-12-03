#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace UniversalArchitecture.Editor
{
    public class RunProject
    {
        [MenuItem("Project/Run  %#r", false,1)]
        private static void RunAllProject()
        {
            if (EditorApplication.isPlaying)
            {
                EditorApplication.isPlaying = false;
                return;
            }
            if (EditorBuildSettings.scenes.Length > 0 && EditorBuildSettings.scenes[0] != null)
            {
                EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
                EditorSceneManager.OpenScene(EditorBuildSettings.scenes[0].path);
                EditorApplication.isPlaying = true;
            }
            else
            {
                Debug.Log("Can't run! EditorBuildSettings.scenes==0");
            }
        }
    }
    
}

#endif