using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameFramework
{
    public static class Main
    {
        private static string _sceneAfterPreloadScene;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void InitOnLoad()
        {
            string currentScene = SceneManager.GetActiveScene().name;
            if (currentScene == "_preload")
            {
                return;
            }
            
            _sceneAfterPreloadScene = currentScene;
            SceneManager.LoadScene("_preload", LoadSceneMode.Single);
            SceneManager.LoadScene(_sceneAfterPreloadScene, LoadSceneMode.Single);
        }
    }
}
