using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameFramework
{
    public class GameInstance : Singleton<GameInstance>
    {
        [SerializeField] private GameFrameworkSettings _settings = null;
        public GameFrameworkSettings Settings => _settings; 

        public PlayerInputController PlayerInputController { get; private set; }
        public GameModeBase GameMode { get; private set; }

        protected virtual void Awake()
        {
            Debug.AssertFormat(Settings.RewiredInputManagerPrefab != null,
                $"{nameof(Rewired.InputManager)} not assigned in {nameof(GameFrameworkSettings)}");
            
            // Instantiate Rewired input manager
            Instantiate(Settings.RewiredInputManagerPrefab);

            // Instantiate the input manager
            GameObject inputManagerGo = new GameObject($"_{nameof(PlayerInputController)}");
            PlayerInputController = inputManagerGo.AddComponent<PlayerInputController>();
            DontDestroyOnLoad(inputManagerGo);

            SceneManager.activeSceneChanged += OnSceneChanged;

            // Initial scene does not get handled by the delegate callback
            //SceneManager.LoadScene("CoolScene");
        }

        private void OnSceneChanged(Scene oldScene, Scene newScene)
        {
            foreach (SceneGameModePair sceneGameModePair in Settings.GameModeOverrides)
            {
                if (sceneGameModePair.Scene.ScenePath != newScene.path ||
                    sceneGameModePair.GameModePrefab == null)
                {
                    continue;
                }
                
                // Spawn custom game override 
                GameMode = Instantiate(sceneGameModePair.GameModePrefab);
                return;
            }

            // No game mode override found for the scene, spawn default game mode 
            GameMode = Instantiate(Settings.DefaultGameMode);
        }
    }
}
