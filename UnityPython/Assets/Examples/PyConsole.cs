using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class PyConsole : MonoBehaviour {
    [SerializeField] PyInputField input;
    [SerializeField] Text consoleText;

    List<Log> unityLogs = new List<Log>();
    List<Log> expressionLogs = new List<Log>();

    private void Start() {
        input.Init(this);
    }

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
        AddUnityLog(condition, color);
    }

    public void SetExecuteAction(Action<string> executeInputCommand) {
        input.SetExecuteAction(executeInputCommand);
    }

    void AddUnityLog(string text, Color color) {
        Log log = new Log(Time.realtimeSinceStartup, text, color);

        unityLogs.Add(log);

        DisplayLog(log);
    }

    public void AddExpressionLog(string text) {
        Log log = new Log(Time.realtimeSinceStartup, text, Color.blue);

        expressionLogs.Add(log);

        DisplayLog(log);
    }

    void DisplayLog(Log log) {
        int lenght = 200;
        var output = Regex.Split(log.text, @"(.{1," + lenght + @"})(?:\s|$)|(.{" + lenght + @"})")
                  .Where(x => x.Length > 0)
                  .ToList();

        consoleText.text += string.Format(" {0} -> ", log.GetTime());
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

    public Log(float time, string text, Color color) {
        this.time = time;
        this.text = text;
        this.color = color;
    }

    public string GetHexColor() {
        string hex = ColorUtility.ToHtmlStringRGBA(color);
        return "#" + hex;
    }

    public string GetTime() {
        TimeSpan timeSpan = TimeSpan.FromSeconds(time);
        string timeText = "";
        if (timeSpan.Hours > 0)
            timeText += timeSpan.Hours + ":";
        if (timeSpan.Minutes > 0)
            timeText += timeSpan.Minutes + ":";

        timeText += timeSpan.Seconds + ":" + timeSpan.Milliseconds.ToString("00");

        return timeText;
    }
}