using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameFramework
{
    public class GameFrameworkSettings : ScriptableObject
    {
        [SerializeField] private GameInstance _gameInstancePrefab;
        public GameInstance GameInstancePrefab
        {
            get { return _gameInstancePrefab; }
        }
    }
    
}
