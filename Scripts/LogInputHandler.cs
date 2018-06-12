namespace OpenLogView
{
    using System;
    using UnityEngine;

    public class LogInputHandler
    {
        private LogView terminal;
        private float touchDelay = 1.5f;
        private float ellapsedTime = 0f;
        public LogInputHandler(LogView t)
        {
            terminal = t;
        }
        public void Update()
        {
#if UNITY_ANDROID || UNITY_IOS
            if (MobileInput())
            {
                terminal.ToggleLogView();
                return;
            }
#elif UNITY_EDITOR || UNITY_STANDALONE
            if (terminal.config.holdShift)
            {
                if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                {
                    CheckKey();
                }
            }
            else
            {
                CheckKey();
            }
#endif
        }

        private void CheckKey()
        {
            if (Input.GetKeyDown(terminal.config.logHotkey))
            {
                terminal.ToggleLogView();
            }
        }

        private bool MobileInput()
        {
            if (Input.touchCount == terminal.config.mobileTouchCount)
            {
                ellapsedTime += Time.deltaTime;
            }
            else ellapsedTime = 0;
            if (ellapsedTime > touchDelay)
            {
                ellapsedTime = 0;
                if (terminal.displayLogView)
                {
                    return false;
                }
                return true;
            }
            else return false;
        }
    }
}