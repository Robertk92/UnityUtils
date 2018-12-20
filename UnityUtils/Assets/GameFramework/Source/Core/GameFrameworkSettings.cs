
using System.Collections.Generic;
using UnityEngine;

namespace GameFramework
{
    public class GameFrameworkSettings : ScriptableObject
    {
        [SerializeField]
        private GameInstance _gameInstancePrefab = null;
        public GameInstance GameInstancePrefab => _gameInstancePrefab;

        [SerializeField]
        private Rewired.InputManager _rewiredInputManagerPrefab = null;
        public Rewired.InputManager RewiredInputManagerPrefab => _rewiredInputManagerPrefab;

        [SerializeField]
        private GameModeBase _defaultGameMode;
        public GameModeBase DefaultGameMode => _defaultGameMode;
        
        [SerializeField, SceneSettingsArray]
        private List<SceneGameModePair> _gameModeOverrides = new List<SceneGameModePair>();
        public List<SceneGameModePair> GameModeOverrides => _gameModeOverrides;
    }
}
