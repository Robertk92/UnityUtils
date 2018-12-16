using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



namespace GameFramework
{
    public class GameFrameworkSettings : ScriptableObject
    {
        [SerializeField] private GameInstance _gameInstancePrefab = null;
        public GameInstance GameInstancePrefab => _gameInstancePrefab;

        [SerializeField] private GameObject _playerPrefab = null;
        public  GameObject PlayerPrefab => _playerPrefab;
    }
    
}
