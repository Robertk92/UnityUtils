using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

namespace GameFramework
{
    [InitializeOnLoad]
    public class CompileCommandEditorWindow : EditorWindow
    {
        public static CompileCommandEditorWindow Instance { get; private set; }
        public static bool IsOpen
        {
            get { return Instance != null; }
        }

        public const int Width = 500;
        public const int Height = 50;//

        private static bool _wasCompiling;
        
        static CompileCommandEditorWindow()
        {
            EditorApplication.update += () =>
            {
                if (EditorApplication.isCompiling && !_wasCompiling)
                {
                    // Start
                    CompileCommandEditorWindow window = GetWindow<CompileCommandEditorWindow>();
                    
                    window.position = new Rect(
                        (int)(Screen.currentResolution.width / 2) - (Width / 2),
                        (int)(Screen.currentResolution.height / 2) - (Height / 2),
                        Width, Height);

                    window.ShowPopup();

                    _wasCompiling = true;
                }

                if (!EditorApplication.isCompiling && _wasCompiling)
                {
                    // Done
                    
                }
            };
        }

        [UnityEditor.Callbacks.DidReloadScripts]
        private static void OnReloadScripts()
        {
            GetWindow<CompileCommandEditorWindow>().Close();
            _wasCompiling = false;
        }
 
        private void OnGUI()
        {
            int oldSize = GUI.skin.label.fontSize;
            GUI.skin.label.fontSize = 32;
            GUILayout.Label("Compiling...");
            GUI.skin.label.fontSize = oldSize;
        }
    }
}