namespace OpenLogView
{
    using System;
    using UnityEngine;

    public class LogGUI
    {
        private LogView logView;
        private LogConfig config;
        private GUIStyle buttonStyle;

        public GUIStyle logStyle { get; private set; }

        public LogGUI(LogView logView)
        {
            this.logView = logView;
            this.config = logView.config;
            logStyle = new GUIStyle();
            logStyle.font = config.font;
            logStyle.fontSize = 16;
            logStyle.richText = true;
            logStyle.normal.textColor = config.infoColor;
            logStyle.hover.textColor = config.warnColor;
            logStyle.active.textColor = config.warnColor;
            logStyle.onHover.textColor = config.warnColor;
            logStyle.onActive.textColor = config.warnColor;
        }

        internal void OnGUI()
        {
            if (buttonStyle == null)
            {
                buttonStyle = new GUIStyle(GUI.skin.box);
                buttonStyle.font = config.font;
                buttonStyle.fixedWidth = 100;
            }

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("clear", buttonStyle))
            {
                logView.Clear();
            }
            if (GUILayout.Button("close", buttonStyle))
            {
                logView.Hide();
            }
            GUILayout.EndHorizontal();
            GUILayout.Label(logView.history + logView.consoleLine + logView.inputText, logStyle);
        }

    }
}