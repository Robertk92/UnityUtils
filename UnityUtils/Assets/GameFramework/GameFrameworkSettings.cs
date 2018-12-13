using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameFramework
{
    [CreateAssetMenu(menuName = "GameFramework/GameFramework Settings")]
    public class GameFrameworkSettings : ScriptableObject
    {
        [SerializeField]
        private GameInstance _gameInstancePrefab;
        public GameInstance GameInstancePrefab
        {
            get { return _gameInstancePrefab; }
        }
    }
}
