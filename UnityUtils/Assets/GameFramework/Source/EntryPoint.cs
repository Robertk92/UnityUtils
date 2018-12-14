using UnityEngine;
using Object = UnityEngine.Object;

namespace GameFramework
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField]
        public GameFrameworkSettings GameFrameworkSettings;
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Main()
        {
            // Temp create entry point gameObject for retrieving game instance prefab default value
            GameObject epGo = new GameObject(typeof(EntryPoint).Name);
            EntryPoint entry = epGo.AddComponent<EntryPoint>();

            Debug.AssertFormat(entry.GameFrameworkSettings.GameInstancePrefab != null,
                string.Format("No {0} prefab assigned in {1}", typeof(GameInstance).Name,
                    typeof(GameFrameworkSettings).Name));

            Object gameInstancePrefabs = entry.GameFrameworkSettings.GameInstancePrefab.gameObject;
            
            Debug.AssertFormat(Object.FindObjectsOfType<GameInstance>().Length == 0,
                string.Format("{0} found in the scene, this is not allowed. " +
                "Remove the {0} from your scene (it will be created automatically before scene load)", typeof(GameInstance).Name));
            
            Debug.AssertFormat(gameInstancePrefabs != null,
                string.Format("{0} prefab not found in {1} (default value field)", typeof(GameInstance).Name, typeof(EntryPoint).Name));
            
            // Instantiate the game instance
            GameObject gameInstanceGo = Object.Instantiate(gameInstancePrefabs) as GameObject;
            Debug.AssertFormat(gameInstanceGo != null, string.Format("Failed to instantiate {0}", typeof(GameInstance).Name));
            gameInstanceGo.name = string.Format("_{0}", typeof(GameInstance).Name);
            DontDestroyOnLoad(gameInstanceGo);

            // Destroy temp entry point gameObject
            Destroy(epGo);
        }
    }
}
