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

            Debug.AssertFormat(settingsLoader.Settings.GamePrefab != null,
                $"No {typeof(GameBase).Name} prefab assigned on {typeof(GameFrameworkSettings).Name}");
            
            Object gamePrefab = settingsLoader.Settings.GamePrefab.gameObject;
            
            Debug.AssertFormat(Object.FindObjectsOfType<GameBase>().Length == 0,
                $"{typeof(GameBase).Name} found in the scene, this is not allowed. " +
                $"Remove the {typeof(GameBase).Name} from your scene (it will be created automatically before scene load)");
            
            Debug.AssertFormat(gamePrefab != null,
                $"{typeof(GameBase).Name} prefab not found in {typeof(EntryPoint).Name} (default value field)");
            
            // Instantiate the game instance
            GameObject gameGo = Object.Instantiate(gamePrefab) as GameObject;
            Debug.AssertFormat(gameGo != null, $"Failed to instantiate {typeof(GameBase).Name}");
            gameGo.name = $"_{typeof(GameBase).Name}";
            Object.DontDestroyOnLoad(gameGo);
        }
    }
}
