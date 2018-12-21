using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace GameFramework
{
    public class PlayerSpawnMenuItem 
    {
        
        [MenuItem("GameObject/Create Other/GameFramework/PlayerSpawn")]
        private static void Create()
        {   
            GameObject go = new GameObject("PlayerSpawn");
            go.AddComponent<PlayerSpawn>();
            EditorUtils.PlaceInFrontOfEditorCamera(go);
        }
    }
}
