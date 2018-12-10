
using UnityEngine;

namespace Utils.Diagnostics
{
    [CreateAssetMenu(menuName = "Utils/Diagnostics/Diagnostic Settings")]
    public class SO_DiagnosticSettings : ScriptableObject
    {
        [SerializeField]
        private Color _messageColor = Color.white;
        public  Color MessageColor { get { return _messageColor; } }

        [SerializeField]
        private Color _warningColor = Color.yellow;
        public Color WarningColor { get { return _warningColor; } }

        [SerializeField]
        private Color _errorColor = Color.red;
        public Color ErrorColor { get { return _errorColor; } }

        [SerializeField]
        [Tooltip("The time (in seconds) before a screen message is removed")]
        private float _screenMessageTimeShown = 5.0f;
        public float ScreenMessageTimeShown { get { return _screenMessageTimeShown; } }
    }
}
