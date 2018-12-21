using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GameFramework
{
    public class GameModeBase : MonoBehaviour
    {
        [SerializeField]
        private GameObject _playerPrefab = null;
        public GameObject PlayerPrefab => _playerPrefab;

        [SerializeField]
        private bool _autoSpawnPlayer = true;
        public bool AutoSpawnPlayer => _autoSpawnPlayer;
        
        protected virtual void Awake()
        {
            GameFrameworkSettings settings = GameBase.Instance.Settings;
            if (AutoSpawnPlayer)
            {
                if (PlayerPrefab != null)
                {
                    SpawnPlayer(PlayerPrefab);
                }
            }
        }

        /// <summary>
        /// Spawns the player gameObject on a free PlayerSpawn in the scene
        /// </summary>
        /// <param name="playerPrefab">The player prefab to spawn</param>
        /// <param name="inputPlayer">Sets the InputPlayer field on the InputReceiver of the player prefab (if applicable)</param>
        /// <param name="autoPossess">If true, looks for the InputReceiver component on the player and has the PlayerInputController possess it</param>
        /// <returns></returns>
        public GameObject SpawnPlayer(GameObject playerPrefab, InputPlayer inputPlayer = InputPlayer.P1, bool autoPossess = true)
        {
            Tuple<Vector3, Quaternion> spawn = GetBestPlayerSpawnLocationAndRotation();
            Debug.AssertFormat(spawn != null, $"Failed to spawn player: no {nameof(PlayerSpawn)} found in the scene");
            
            GameObject player = Instantiate(playerPrefab, spawn.Item1, spawn.Item2);
            InputReceiver inputReceiver = player.GetComponent<InputReceiver>();
            inputReceiver.InputPlayer = inputPlayer;

            if (inputReceiver != null && autoPossess)
            {
                GameBase.Instance.PlayerInputController.Possess(inputReceiver);
            }

            return player;
        }

        private Tuple<Vector3, Quaternion> GetBestPlayerSpawnLocationAndRotation()
        {
            PlayerSpawn[] spawns = FindObjectsOfType<PlayerSpawn>();
            foreach (PlayerSpawn playerSpawn in spawns)
            {
                if (playerSpawn.IsFree)
                {
                    return new Tuple<Vector3, Quaternion>(playerSpawn.transform.position, playerSpawn.transform.rotation);
                }
            }

            if (spawns.Length == 0)
            {
                return null;
            }

            
            PlayerSpawn randomPlayerSpawn = spawns[Random.Range(0, spawns.Length)];
            return new Tuple<Vector3, Quaternion>(randomPlayerSpawn.transform.position, randomPlayerSpawn.transform.rotation);
        }
    }
}
