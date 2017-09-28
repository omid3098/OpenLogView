using System;
using UnityEngine;
[CreateAssetMenu()]
public class LogConfig : ScriptableObject
{
    public KeyCode logHotkey;
    public int mobileTouchCount;
    public string arrow;
    public string backslash;
    public string console;
    public Font font;
    public Color infoColor;
    public Color warnColor;
    public Color errorColor;
}
