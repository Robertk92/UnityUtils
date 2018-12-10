
using UnityEngine;
using Utils.Diagnostics;

namespace Utils
{
    [CreateAssetMenu(menuName = "Utils/Utils Settings")]
    public class SO_UtilsSettings : ScriptableObject
    {
        [SerializeField]
        private SO_DiagnosticSettings _diagnosticSettings;
        public SO_DiagnosticSettings DiagnosticSettings { get { return _diagnosticSettings; } }

        [SerializeField] private bool _printDebugLogsToScreen = true;
        public bool PrintDebugLogsToScreen { get { return _printDebugLogsToScreen; } }
    }
}
