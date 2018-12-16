using UnityEngine;

namespace GameFramework
{
    public class GameInstance : Singleton<GameInstance>
    {
        [SerializeField] private GameFrameworkSettings _settings = null;
        public GameFrameworkSettings Settings => _settings; 

        public InputManager InputManager { get; private set; }

        protected virtual void Awake()
        {
            // Instantiate the input manager
            GameObject inputManagerGo = new GameObject("_InputManager");
            InputManager = inputManagerGo.AddComponent<InputManager>();
            DontDestroyOnLoad(inputManagerGo);
            
            if (Settings.PlayerPrefab != null)
            {
                // Instantiate the player and set as input receiver (if the prefab has a InputReceiver component)
                GameObject player = Instantiate(Settings.PlayerPrefab);
                InputReceiver playerInputReceiver = player.GetComponent<InputReceiver>();
                if (playerInputReceiver != null)
                {
                    InputManager.Receiver = playerInputReceiver;
                }
            }
        }
    }
}
