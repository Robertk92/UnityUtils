using System.IO;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GameFramework
{
    public static class EntryPoint
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Main()
        {
            string settingsLoaderResourcesPath = "SettingsLoader";
            SettingsLoader settingsLoader = Resources.Load<SettingsLoader>(settingsLoaderResourcesPath);

            Debug.AssertFormat(settingsLoader != null,
                string.Format("{0} not found in {1}", typeof(SettingsLoader).Name,
                Path.Combine("Resources", settingsLoaderResourcesPath)));

            Debug.AssertFormat(settingsLoader.Settings != null,
                string.Format("{0} not assigned on {1}", typeof(GameFrameworkSettings).Name,
                typeof(SettingsLoader).Name));

            Debug.AssertFormat(settingsLoader.Settings.GameInstancePrefab != null,
                string.Format("No {0} prefab assigned on {1}", typeof(GameInstance).Name,
                    typeof(GameFrameworkSettings).Name));
            
            Object gameInstancePrefabs = settingsLoader.Settings.GameInstancePrefab.gameObject;
            
            Debug.AssertFormat(Object.FindObjectsOfType<GameInstance>().Length == 0,
                string.Format("{0} found in the scene, this is not allowed. " +
                "Remove the {0} from your scene (it will be created automatically before scene load)", typeof(GameInstance).Name));
            
            Debug.AssertFormat(gameInstancePrefabs != null,
                string.Format("{0} prefab not found in {1} (default value field)", typeof(GameInstance).Name, typeof(EntryPoint).Name));
            
            // Instantiate the game instance
            GameObject gameInstanceGo = Object.Instantiate(gameInstancePrefabs) as GameObject;
            Debug.AssertFormat(gameInstanceGo != null, string.Format("Failed to instantiate {0}", typeof(GameInstance).Name));
            gameInstanceGo.name = string.Format("_{0}", typeof(GameInstance).Name);
            Object.DontDestroyOnLoad(gameInstanceGo);
        }
    }
}
