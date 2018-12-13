
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GameFramework
{
    
    public class GameFrameworkEditorWindow : EditorWindow
    {
        [MenuItem("Window/Game Framework/Settings")]
        private static void Init()
        {
            GameFrameworkEditorWindow window = GetWindow<GameFrameworkEditorWindow>();
            window.titleContent = new GUIContent("Game Framework Settings");
            window.Show();
        }

        


        private int _gameInstanceIndex;
        private static string _gameInstanceTypeName;
        public static string GameInstanceTypeName
        {
            get { return _gameInstanceTypeName; }
        }

        private static Object _gameInstancePrefab;

        public static Object GameInstancePrefab
        {
            get { return _gameInstancePrefab; }
        }

        private void OnGUI()
        {
            Assembly ass = Assembly.Load(new AssemblyName("Assembly-CSharp"));

            List<string> typeStrings = new List<string>();
            foreach (Type type in ass.GetTypes())
            {
                if (type.IsSubclassOf(typeof(GameInstance)) || type == typeof(GameInstance))
                {
                    typeStrings.Add(type.FullName);
                }

            }

            
            _gameInstanceIndex = EditorGUILayout.Popup("Game Instance", _gameInstanceIndex, typeStrings.ToArray());
            _gameInstanceTypeName = typeStrings[_gameInstanceIndex];

            _gameInstancePrefab = EditorGUILayout.ObjectField(_gameInstancePrefab, typeof(GameInstance), false);
        }
    }
}
