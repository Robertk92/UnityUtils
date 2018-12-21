using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace GameFramework
{
    [CustomEditor(typeof(PlayerInputController))]
    public class PlayerInputControllerEditor : Editor
    {
        private PlayerInputController _pic;

        private void OnEnable()
        {
            _pic = (PlayerInputController) target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (!Application.isPlaying)
            {
                return;
            }

            EditorGUILayout.HelpBox("Receivers", MessageType.Info);
            
            foreach (KeyValuePair<InputPlayer, InputReceiver> kvp in _pic.Receivers)
            {
                InputReceiver scenePlayer = null;
                if (kvp.Value != null)
                {
                    scenePlayer = GameObject.Find(kvp.Value.name).GetComponent<InputReceiver>();
                }
                
                scenePlayer = (InputReceiver)EditorGUILayout.ObjectField(
                    new GUIContent(kvp.Key.ToString()), scenePlayer, typeof(InputReceiver), true);
            }
        }
    }
}
