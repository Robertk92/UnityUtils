
using UnityEngine;
using UnityEngine.Serialization;
using Utils.Diagnostics;
using Utils.Generic;

namespace Utils
{
    public class UtilsInstance : Singleton<UtilsInstance>
    {
        [SerializeField] private SO_UtilsSettings _settings;
        public SO_UtilsSettings Settings { get { return _settings; } }

        public ScreenPrinter ScreenPrinter { get; private set; }

        private void Awake()
        {
            if (Settings.PrintDebugLogsToScreen)
            {
                GameObject screenPrinterGo = new GameObject("Screen Printer");
                ScreenPrinter = screenPrinterGo.AddComponent<ScreenPrinter>();
                screenPrinterGo.transform.SetParent(transform);
            }
        }
    }
}

