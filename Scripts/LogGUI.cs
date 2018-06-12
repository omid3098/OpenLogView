namespace OpenLogView
{
    using System;
    using UnityEngine;

    public class LogGUI
    {
        // 15% headers
        // 70% content
        //      40% log titles
        //      30% stack traces
        // 15% footer
        private const int fontSizeSteps = 2;
        private const int numberOfButtons = 5;
        private const int headerPercent = 15;
        private LogView logView;
        private LogConfig config;
        private GUIStyle buttonStyle;
        private int activeIndex;
        public GUIStyle logStyle { get; private set; }
        public GUIStyle logButtonStyle { get; private set; }

        public LogGUI(LogView logView)
        {
            this.logView = logView;
            this.config = logView.config;
            logStyle = new GUIStyle();
            logStyle.font = config.font;
            logStyle.fontSize = 16;
            logStyle.richText = true;
            logStyle.wordWrap = true;
            logStyle.normal.textColor = config.infoColor;
            logStyle.hover.textColor = config.warnColor;
            logStyle.active.textColor = config.warnColor;
            logStyle.onHover.textColor = config.warnColor;
            logStyle.onActive.textColor = config.warnColor;

            logButtonStyle = new GUIStyle();
            logButtonStyle.normal.textColor = config.infoColor;
            logButtonStyle.font = config.font;
            logButtonStyle.fontSize = 16;
            logButtonStyle.richText = true;
            logButtonStyle.wordWrap = true;
            logButtonStyle.fixedHeight = 18;
            logButtonStyle.fixedWidth = Screen.width;

        }

        internal void OnGUI()
        {
            GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "");
            if (buttonStyle == null)
            {
                buttonStyle = new GUIStyle(GUI.skin.box);
                buttonStyle.font = config.font;
                buttonStyle.fontSize = 16;
                buttonStyle.wordWrap = true;
                // devide screen width into 5 buttons. clear/share/close/+/-
                buttonStyle.fixedWidth = Screen.width / numberOfButtons;
                buttonStyle.fixedHeight = logStyle.fontSize + 8;
            }

            // Header buttons
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("clear", buttonStyle)) logView.Clear();
            if (GUILayout.Button("close", buttonStyle)) logView.Hide();
            if (GUILayout.Button("share", buttonStyle)) logView.Share();
            if (GUILayout.Button("-", buttonStyle)) DecreaseFontSize();
            if (GUILayout.Button("+", buttonStyle)) IncreaseFontSize();
            GUILayout.EndHorizontal();

            // Log items
            GUILayout.BeginScrollView(new Vector2(0, Screen.height / headerPercent));
            for (int i = 0; i < logView.logTypes.Count; i++)
            {
                string logType = "";
                switch (logView.logTypes[i])
                {
                    case LogType.Log:
                        logType = string.Format("<color=#{0}>Info</color>", logView.infoColor);
                        break;
                    case LogType.Warning:
                        logType = string.Format("<color=#{0}>Warning</color>", logView.warnColor);
                        break;
                    case LogType.Error:
                    case LogType.Assert:
                        logType = string.Format("<color=#{0}>Error</color>", logView.errorColor);
                        break;
                }
                string logLine = logType + ": " + logView.logTitles[i];
                if (GUILayout.Button(logLine, logButtonStyle))
                {
                    activeIndex = i;
                }
                // Line at the end of each item
                GUILayout.Box(GUIContent.none, CustomGUIStyles.LineLayout, GUILayout.ExpandWidth(true), GUILayout.Height(1f));
            }
            GUILayout.EndScrollView();
            if (logView.logStacks.Count != 0) GUILayout.Label(logView.logStacks[activeIndex], logStyle);
        }

        private void IncreaseFontSize()
        {
            logStyle.fontSize += fontSizeSteps;
            buttonStyle.fontSize += fontSizeSteps;
            buttonStyle.fixedHeight += fontSizeSteps;

            logButtonStyle.fontSize += fontSizeSteps;
            logButtonStyle.fixedHeight += fontSizeSteps;
        }

        private void DecreaseFontSize()
        {
            logStyle.fontSize -= fontSizeSteps;
            buttonStyle.fontSize -= fontSizeSteps;
            buttonStyle.fixedHeight -= fontSizeSteps;

            logButtonStyle.fontSize -= fontSizeSteps;
            logButtonStyle.fixedHeight -= fontSizeSteps;
        }
    }
    public static class CustomGUIStyles
    {
        private static GUIStyle m_line = null;
        //constructor
        static CustomGUIStyles()
        {

            m_line = new GUIStyle("box");
            m_line.border.top = m_line.border.bottom = 1;
            m_line.margin.top = m_line.margin.bottom = 1;
            m_line.padding.top = m_line.padding.bottom = 1;
        }
        public static GUIStyle LineLayout
        {
            get { return m_line; }
        }
    }
}