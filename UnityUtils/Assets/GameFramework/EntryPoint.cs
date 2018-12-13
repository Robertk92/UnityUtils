using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GameFramework
{
    public static class EntryPoint
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Main()
        {
            Object[] gameInstancePrefabs = Resources.FindObjectsOfTypeAll<GameInstance>();

            Debug.AssertFormat(GameObject.FindObjectsOfType<GameInstance>().Length == 0,
                string.Format("GameInstance found in the scene, this is not allowed. " +
                "Remove the GameInstance from your scene (it will be created automatically before scene load)"));
            
            Debug.AssertFormat(gameInstancePrefabs.Length > 0,
                string.Format("GameInstance prefab not found", typeof(GameInstance).Name));

            Debug.AssertFormat(gameInstancePrefabs.Length < 2,
                string.Format("Multiple GameInstance prefabs found, you should not create your own GameInstance prefabs!" +
                "Use the one in the GameFramework"));

            // Instantiate the game instance
            GameObject gameInstanceGo = Object.Instantiate(gameInstancePrefabs[0]) as GameObject;
            Object.DontDestroyOnLoad(gameInstanceGo);
        }
    }
}
