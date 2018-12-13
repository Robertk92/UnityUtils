using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

namespace GameFramework
{
    public class ScreenPrinter : Singleton<ScreenPrinter>
    {
        private readonly List<ScreenMessage> _screenLogs = new List<ScreenMessage>();
        
        private void Awake()
        {    
            Application.logMessageReceived += OnLogMessageReceived;
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
                        logColor = Color.red;
                        break;
                    case LogType.Assert:
                        logColor = Color.red;
                        break;
                    case LogType.Warning:
                        logColor = Color.yellow;
                        break;
                    case LogType.Log:
                        logColor = Color.cyan;
                        break;
                    case LogType.Exception:
                        logColor = Color.red;
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