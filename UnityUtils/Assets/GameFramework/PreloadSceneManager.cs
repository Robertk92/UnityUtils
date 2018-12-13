using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

namespace GameFramework
{
    public class PreloadSceneManager : MonoBehaviour
    {
        [SerializeField]
        private GameFrameworkSettings _gameFrameworkSettings;

        private void Awake()
        {
            Debug.Log("YEA");
        }
    }
}