using System;
using UnityEngine;
[CreateAssetMenu()]
public class LogConfig : ScriptableObject
{
    [Header("PC Config")]
    public KeyCode logHotkey;
    public bool holdShift;
    [Space]
    [Header("Mobile Config")]
    public int mobileTouchCount;
    [Space]
    [Header("Console Config")]
    public string arrow;
    public string backslash;
    public string console;
    public Font font;
    public Color infoColor;
    public Color warnColor;
    public Color errorColor;
    [Space]
    [Header("Report Setting")]
    public string supportEmail;
    public string EmailTitle;
}
