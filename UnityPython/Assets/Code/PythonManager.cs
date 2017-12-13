using IronPython.Hosting;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace UnityPythonConsole.Assets.Code {
    public class PythonManager : MonoBehaviour {
        [SerializeField] PyConsole pyConsole;
        [SerializeField] UnityMethodsEvents unityMethodsEvents;

        Microsoft.Scripting.Hosting.ScriptEngine engine;
        Microsoft.Scripting.Hosting.ScriptScope scope;

        string pythonScript;
        private void Awake() {
            StartCoroutine(GetPythonScript());
            pyConsole.SetExecuteAction(ExecuteInputCommand);
        }

        private void ExecuteInputCommand(string expression) {
            engine.Execute(expression, scope);
        }

        private void Start() {
            engine = Python.CreateEngine();
            engine.Runtime.LoadAssembly(typeof(GameObject).Assembly);

            scope = engine.CreateScope();
            scope.SetVariable("pyConsole", pyConsole);
            scope.SetVariable("unityEvents", unityMethodsEvents);
            engine.Execute(pythonScript, scope);
        }
        
        IEnumerator GetPythonScript() {
            string filePath = System.IO.Path.Combine(Application.streamingAssetsPath, "python.py");

            string result;
            if (filePath.Contains("://") || filePath.Contains(":///")) {
                UnityEngine.Networking.UnityWebRequest www = UnityEngine.Networking.UnityWebRequest.Get(filePath);
                yield return www.SendWebRequest();
                result = www.downloadHandler.text;
            } else
                result = System.IO.File.ReadAllText(filePath);

            pythonScript = result;
        }
    }
}
