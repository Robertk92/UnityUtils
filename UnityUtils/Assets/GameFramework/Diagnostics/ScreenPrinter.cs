using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

namespace GameFramework
{
    [CreateAssetMenu]
    public class ScreenPrinterContext : ScriptableObject
    {
        [SerializeField]
        private Color _errorColor;
        public Color ErrorColor { get { return _errorColor; } }

        [SerializeField]
        private Color _warningColor;
        public Color WarningColor { get { return _warningColor; } }

        [SerializeField]
        private Color _messageColor;
        public Color MessageColor { get { return _messageColor; } }
    }

    public class ScreenPrinter : MonoBehaviour
    {
        [SerializeField]
        private ScreenPrinterContext _context;
        public ScreenPrinterContext Context { get { return _context; } }

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
                        logColor = Context.ErrorColor;
                        break;
                    case LogType.Assert:
                        logColor = Context.ErrorColor;
                        break;
                    case LogType.Warning:
                        logColor = Context.WarningColor;
                        break;
                    case LogType.Log:
                        logColor = Context.MessageColor;
                        break;
                    case LogType.Exception:
                        logColor = Context.ErrorColor;
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