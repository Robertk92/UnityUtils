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
            const string settingsLoaderResourcesPath = "SettingsLoader";
            SettingsLoader settingsLoader = Resources.Load<SettingsLoader>(settingsLoaderResourcesPath);

            Debug.AssertFormat(settingsLoader != null,
                $"{typeof(SettingsLoader).Name} not found in {Path.Combine("Resources", settingsLoaderResourcesPath)}");

            Debug.AssertFormat(settingsLoader.Settings != null,
                $"{typeof(GameFrameworkSettings).Name} not assigned on {typeof(SettingsLoader).Name}");

            Debug.AssertFormat(settingsLoader.Settings.GameInstancePrefab != null,
                $"No {typeof(GameInstance).Name} prefab assigned on {typeof(GameFrameworkSettings).Name}");
            
            Object gameInstancePrefabs = settingsLoader.Settings.GameInstancePrefab.gameObject;
            
            Debug.AssertFormat(Object.FindObjectsOfType<GameInstance>().Length == 0,
                $"{typeof(GameInstance).Name} found in the scene, this is not allowed. " +
                $"Remove the {typeof(GameInstance).Name} from your scene (it will be created automatically before scene load)");
            
            Debug.AssertFormat(gameInstancePrefabs != null,
                $"{typeof(GameInstance).Name} prefab not found in {typeof(EntryPoint).Name} (default value field)");
            
            // Instantiate the game instance
            GameObject gameInstanceGo = Object.Instantiate(gameInstancePrefabs) as GameObject;
            Debug.AssertFormat(gameInstanceGo != null, $"Failed to instantiate {typeof(GameInstance).Name}");
            gameInstanceGo.name = $"_{typeof(GameInstance).Name}";
            Object.DontDestroyOnLoad(gameInstanceGo);
        }
    }
}
