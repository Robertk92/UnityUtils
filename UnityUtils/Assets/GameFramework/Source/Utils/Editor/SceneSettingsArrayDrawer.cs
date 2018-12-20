using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace GameFramework
{
    [CustomPropertyDrawer(typeof(SceneSettingsArrayAttribute))]
    public class SceneSettingsArrayDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label)
        {     
            GameFrameworkSettings target = property.serializedObject.targetObject as GameFrameworkSettings;
            SceneGameModePair settings = null;
            int index = 0;
            
            if(property.displayName.Contains("Element"))
            {    
                index = int.Parse(property.displayName.Split(' ')[1]);
                if (index < target.GameModeOverrides.Count)
                {
                    settings = target.GameModeOverrides[index];
                }
            }

            string title = "None";

            bool greenlit = false;
            if (settings != null)
            {
                if (settings.Scene != null)
                {
                    if (!string.IsNullOrEmpty(settings.Scene.SceneName))
                    {
                        title = settings.Scene.SceneName;
                        if (settings.Scene.PresentInBuildSettings)
                        {
                            greenlit = true;
                        }
                    }
                }
            }
            
            EditorGUI.BeginProperty(rect, GUIContent.none, property);

            Color oldGuiColor = GUI.contentColor;
            GUI.contentColor = greenlit ? Color.green : Color.red;
            EditorGUI.LabelField(rect, title);
            GUI.contentColor = oldGuiColor;

            EditorGUI.PropertyField(rect, property, GUIContent.none, true);

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            const int extra = 75;
            if (property.isExpanded)
            {
                return base.GetPropertyHeight(property, label) + extra;
            }
            return base.GetPropertyHeight(property, label);
        }
    }
}
