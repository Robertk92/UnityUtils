using UnityEngine;

namespace GameFramework
{
    public class SettingsLoader : ScriptableObject
    {
        [SerializeField] private GameFrameworkSettings _settings = null;
        public GameFrameworkSettings Settings => _settings;
    }
}
