using UnityEngine;

namespace GameFramework
{
    [CreateAssetMenu]
    public class SettingsLoader : ScriptableObject
    {
        [SerializeField]
        private GameFrameworkSettings _settings;
        public GameFrameworkSettings Settings { get { return _settings; } }
    }
}
