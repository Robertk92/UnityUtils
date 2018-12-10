using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using Utils.Diagnostics;

namespace Utils.Diagnostics
{
    public class ScreenPrinter : MonoBehaviour
    {
        private readonly List<ScreenMessage> _screenLogs = new List<ScreenMessage>();
        
        private void Awake()
        {
            
            Application.logMessageReceived += OnLogMessageReceived;
            Debug.LogWarning("Hallo");
        }

        private void OnLogMessageReceived(string condition, string stackTrace, LogType type)
        {
            string[] stackTraceLines = Regex.Split(stackTrace, "\r\n|\r|\n");
            string fileAndLine = stackTraceLines[1];
            _screenLogs.Add(new ScreenMessage(condition, fileAndLine, type));
        }

        private void OnGUI()
        {
            Color guiColor = GUI.color;

            foreach (ScreenMessage screenMessage in _screenLogs)
            {
                Color logColor = Color.white;
                switch (screenMessage.LogType)
                {
                    case LogType.Error:
                        logColor = UtilsInstance.Instance.Settings.DiagnosticSettings.ErrorColor;
                        break;
                    case LogType.Assert:
                        logColor = UtilsInstance.Instance.Settings.DiagnosticSettings.ErrorColor;
                        break;
                    case LogType.Warning:
                        logColor = UtilsInstance.Instance.Settings.DiagnosticSettings.WarningColor;
                        break;
                    case LogType.Log:
                        logColor = UtilsInstance.Instance.Settings.DiagnosticSettings.MessageColor;
                        break;
                    case LogType.Exception:
                        logColor = UtilsInstance.Instance.Settings.DiagnosticSettings.ErrorColor;
                        break;
                }

                Color prevContentColor = GUI.contentColor;
                GUI.contentColor = logColor;
                GUILayout.Label(string.Format("{0} ({1})", screenMessage.Message, screenMessage.StackLine));
                GUILayout.Label(screenMessage.StackLine);
                GUI.contentColor = prevContentColor;
            }

            GUI.color = guiColor;
        }
    }
}