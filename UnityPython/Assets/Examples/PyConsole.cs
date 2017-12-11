using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class PyConsole : MonoBehaviour {
    [SerializeField] InputField input;
    [SerializeField] Button button;
    [SerializeField] Text consoleText;

    List<Log> unityLogs = new List<Log>();

    void OnEnable() {
        Application.logMessageReceived += HandleLog;
    }
    void OnDisable() {
        Application.logMessageReceived -= HandleLog;
    }

    private void HandleLog(string condition, string stackTrace, LogType type) {
        Color color = Color.black;
        switch (type) {
            case LogType.Error:
                color = Color.red;
                break;
            case LogType.Assert:
                break;
            case LogType.Warning:
                color = Color.yellow;
                break;
            case LogType.Log:
                break;
            case LogType.Exception:
                color = Color.red;
                break;
            default:
                break;
        }
        AddUnityLog(Time.realtimeSinceStartup, condition, color);
    }

    void AddUnityLog(float time, string text, Color color) {
        Log log = new Log();
        log.time = time;
        log.text = text;
        log.color = color;

        unityLogs.Add(log);

        DisplayLog(log);
    }

    void DisplayLog(Log log) {
        int lenght = 200;
        var output = Regex.Split(log.text, @"(.{1," + lenght + @"})(?:\s|$)|(.{" + lenght + @"})")
                  .Where(x => x.Length > 0)
                  .ToList();

        consoleText.text += string.Format(" {0:0.0} -> ", log.time);
        for (int i = 0; i < output.Count; i++) {
            if (i > 0)
                consoleText.text += "\t\t";
            consoleText.text += string.Format("<color={0}>{1}</color>\n", log.GetHexColor(), output[i]); 
        }
    }
}

internal class Log {
    public string text;
    public float time;
    public Color color = Color.black;

    public string GetHexColor() {
        string hex = ColorUtility.ToHtmlStringRGBA(color);
        return "#" + hex;
    }
}