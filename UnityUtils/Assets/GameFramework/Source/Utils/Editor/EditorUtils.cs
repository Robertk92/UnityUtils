using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace GameFramework
{
    public static class EditorUtils 
    {
        public static void PlaceInFrontOfEditorCamera(GameObject gameObject)
        {
            if (SceneView.lastActiveSceneView == null)
            {
                return;
            }

            if (SceneView.lastActiveSceneView.camera == null)
            {
                return;
            }

            Transform editorCam = SceneView.lastActiveSceneView.camera.transform;
            gameObject.transform.position = editorCam.position + (editorCam.forward * 10.0f);
        }
    }
}
