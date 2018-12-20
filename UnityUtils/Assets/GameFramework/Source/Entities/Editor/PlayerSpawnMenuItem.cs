using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace GameFramework
{
    public class PlayerSpawnMenuItem 
    {
        [MenuItem(itemName: "PlayerSpawn", menuItem = "GameObject/GameFramework/PlayerSpawn")]
        private static void Create()
        {
            GameObject go = new GameObject("PlayerSpawn");
            go.AddComponent<PlayerSpawn>();
        }
    }
}
