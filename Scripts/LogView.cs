namespace OpenLogView
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text.RegularExpressions;
    using UnityEngine;
    public class LogView : MonoBehaviour
    {
        [SerializeField] public LogConfig config;
        public bool displayLogView { get; private set; }
        public string inputText { get; private set; }
        public string history { get; private set; }
        public string consoleLine { get { return (config.console + config.backslash + config.arrow + " "); } }
        private LogInputHandler inputHandler;
        private LogGUI logGui;
        private string infoColor;
        private string warnColor;
        private string errorColor;
        public TouchScreenKeyboard touchScreenKeyboard;

        void Awake()
        {
            DontDestroyOnLoad(this);
            if (config == null) config = Resources.Load<LogConfig>("Config/ZSH");
            if (config.mobileTouchCount <= 0) config.mobileTouchCount = 4;
            inputHandler = new LogInputHandler(this);
            logGui = new LogGUI(this);

            infoColor = ColorUtility.ToHtmlStringRGBA(config.infoColor);
            warnColor = ColorUtility.ToHtmlStringRGBA(config.warnColor);
            errorColor = ColorUtility.ToHtmlStringRGBA(config.errorColor);
        }
        void OnEnable()
        {
            Application.logMessageReceived += HandleLog;
        }
        void OnDisable()
        {
            Application.logMessageReceived -= HandleLog;
        }

        private void HandleLog(string logString, string stackTrace, LogType type)
        {
            string logType = "";
            switch (type)
            {
                case LogType.Log:
                    logType = string.Format("<color=#{0}>i:</color>", infoColor);
                    break;
                case LogType.Warning:
                    logType = string.Format("<color=#{0}>w:</color>", warnColor);
                    break;
                case LogType.Error:
                case LogType.Assert:
                    logType = string.Format("<color=#{0}>e:</color>", errorColor);
                    break;
            }
            inputText += logType + " " + logString + '\n' + consoleLine;
        }

        internal void Hide()
        {
            displayLogView = false;
        }

        void OnGUI()
        {
            if (!displayLogView) return;
            logGui.OnGUI();
        }

        void Update()
        {
            inputHandler.Update();
        }

        internal void Clear()
        {
            StartCoroutine(ClearLogCoroutine());
        }

        private IEnumerator ClearLogCoroutine()
        {
            yield return new WaitForEndOfFrame();
            inputText = "";
        }

        internal void ToggleLogView()
        {
            displayLogView = !displayLogView;
        }

        internal void OnBackSpacePressed()
        {
            if (inputText.Length >= 1) inputText = inputText.Substring(0, inputText.Length - 1);
        }
    }
}