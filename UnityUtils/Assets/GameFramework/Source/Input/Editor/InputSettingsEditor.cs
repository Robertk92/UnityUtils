using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace GameFramework
{
    [CustomEditor(typeof(InputSettings))]
    public class InputSettingsEditor : EditorWindow
    {
        [MenuItem("GameFramework/Input Settings")]
        private static void OpenInputSettings()
        {
            InputSettingsEditor inputSettings = GetWindow<InputSettingsEditor>();
            inputSettings.titleContent = new GUIContent(nameof(InputSettings));
            inputSettings.Show();
        }

        private void OnGUI()
        {
            
        }
    }
}
