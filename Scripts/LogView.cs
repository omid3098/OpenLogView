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

        public void Share()
        {
            Debug.Log("Sharing Log...");
            // #if UNITY_EDITOR || UNITY_STANDALONE || UNITY_IOS
            // if standalone editor or ios, send email
            var result = Regex.Replace(inputText, @"<[^>]*>", String.Empty);
            Application.OpenURL("mailto:" + config.supportEmail + "?subject=" + config.EmailTitle + "&body=" + MyEscapeURL(result));
            //execute the below lines if being run on a Android device
            // #elif UNITY_ANDROID
            //             Debug.Log("Android Platform");
            //             AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
            //             //Reference of AndroidJavaObject class for intent
            //             AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
            //             //call setAction method of the Intent object created
            //             intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
            //             //set the type of sharing that is happening
            //             intentObject.Call<AndroidJavaObject>("setType", "text/plain");
            //             //add data to be passed to the other activity i.e., the data to be sent
            //             intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_SUBJECT"), config.EmailTitle);
            //             intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), inputText);
            //             //get the current activity
            //             AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            //             AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
            //             //start the activity by sending the intent data
            //             currentActivity.Call("startActivity", intentObject);
            // #endif
        }
        string MyEscapeURL(string url)
        {
            return WWW.EscapeURL(url).Replace("+", "%20");
        }
    }
}