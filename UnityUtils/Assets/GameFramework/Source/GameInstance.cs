using UnityEngine;

namespace GameFramework
{
    public class GameInstance : Singleton<GameInstance>
    {
        [SerializeField]
        private GameFrameworkSettings _settings;
        public GameFrameworkSettings Settings { get { return _settings; } }

        protected virtual void Awake()
        {
            
        }
    }
}
