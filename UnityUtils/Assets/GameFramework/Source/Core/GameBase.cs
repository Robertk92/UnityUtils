using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameFramework
{
    public class GameBase : Singleton<GameBase>
    {
        [SerializeField] private GameFrameworkSettings _settings = null;
        public GameFrameworkSettings Settings => _settings; 

        public PlayerInputController PlayerInputController { get; private set; }
        public GameModeBase GameMode { get; private set; }

        protected virtual void Awake()
        {
            Debug.AssertFormat(Settings.RewiredInputManagerPrefab != null,
                $"{nameof(Rewired.InputManager)} not assigned in {nameof(GameFrameworkSettings)}");
            
            GameObjectUtils.EnsureNoInstance<Rewired.InputManager>();
            // Instantiate Rewired input manager
            Instantiate(Settings.RewiredInputManagerPrefab);

            GameObjectUtils.EnsureNoInstance<PlayerInputController>();
            // Instantiate the PlayerInputController
            GameObject playerInputControllerGo = new GameObject($"_{nameof(PlayerInputController)}");
            PlayerInputController = playerInputControllerGo.AddComponent<PlayerInputController>();
            DontDestroyOnLoad(playerInputControllerGo);

            SceneManager.activeSceneChanged += OnSceneChanged;
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
