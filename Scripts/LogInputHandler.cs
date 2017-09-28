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
            if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
            {
                if (MobileInput())
                {
                    terminal.ToggleLogView();
                    return;
                }
            }
            if (Input.GetKeyDown(terminal.config.logHotkey))
            {
                terminal.ToggleLogView();
                return;
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