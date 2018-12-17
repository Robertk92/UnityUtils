using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace GameFramework
{
    public class InputSettingsEditorWindow : EditorWindow
    {
        [MenuItem("GameFramework/Input Settings")]
        private static void OpenInputSettings()
        {
            InputSettingsEditorWindow inputSettings = GetWindow<InputSettingsEditorWindow>();
            inputSettings.titleContent = new GUIContent(nameof(InputSettings));
            inputSettings.Show();
        }

        private void OnGUI()
        {
            GUILayout.Label("Input Settings");
        }
    }
}
