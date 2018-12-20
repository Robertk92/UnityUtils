using System.Collections;
using System.Collections.Generic;
using GameFramework;
using UnityEngine;

public class SampleGameMode : GameModeBase
{
    [SerializeField] private Player _playerPrefab = null;

    private void Start()
    {
        if (_playerPrefab != null)
        {
            for (int i = 0; i < 4; i++)
            {
                SpawnPlayer(_playerPrefab.gameObject, (InputPlayer)i);
            }
            
        }
    }
}
