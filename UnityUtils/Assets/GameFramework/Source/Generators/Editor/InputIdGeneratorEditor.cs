using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEditor;
using System.IO;

namespace GameFramework
{
    public class InputIdGeneratorEditor 
    {
        [MenuItem("GameFramework/Re-generate input id's")]
        private static void UpdateInputIds()
        {
            SerializedObject serializedObject = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/InputManager.asset")[0]);
            SerializedProperty axesProperty = serializedObject.FindProperty("m_Axes");
            
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("using System.Collections.Generic;");
            builder.AppendLine("namespace GameFramework");
            builder.AppendLine("{");
            builder.AppendLine("\tpublic enum InputId");
            builder.AppendLine("\t{");
            
            Dictionary<string, string> cache = new Dictionary<string, string>();
            for (int i = 0; i < axesProperty.arraySize; i++)
            {
                string axisName = axesProperty.GetArrayElementAtIndex(i).displayName;
                string axisEnumName = axisName.Replace(' ', '_');

                if (cache.ContainsKey(axisName))
                {
                    continue;
                }

                cache.Add(axisName, axisEnumName);
                builder.AppendLine($"\t\t{axisEnumName},");
            }
            
            builder.AppendLine("\t}");
            builder.AppendLine("");
            builder.AppendLine("\tpublic static class GeneratedInput");
            builder.AppendLine("\t{");
            builder.AppendLine("\t\tprivate static readonly Dictionary<InputId, string> _ids = new Dictionary<InputId, string>");
            builder.AppendLine("\t\t{");
            foreach (var kvp in cache)
            {
                builder.AppendLine($"\t\t\t {{InputId.{kvp.Value},\"{kvp.Key}\"}},");
            }
            
            builder.AppendLine("\t\t};");

            builder.AppendLine("\t\tpublic static Dictionary<InputId, string> Ids => _ids;");

            builder.AppendLine("\t}");
            builder.AppendLine("}");
            
            string ioPath = $"{Application.dataPath}/GameFramework/Generated/InputId.cs";

            Debug.AssertFormat(File.Exists(ioPath), "Missing InputId file");
            
            File.WriteAllText(ioPath, builder.ToString());
            AssetDatabase.Refresh();
        }
    }
}
