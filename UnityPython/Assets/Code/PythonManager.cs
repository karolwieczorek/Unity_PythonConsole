using IronPython.Hosting;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace UnityPython.Assets.Examples {
    public class PythonManager : MonoBehaviour {
        [SerializeField] PyConsole pyConsole;

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
            engine.Execute(pythonScript, scope);

            Debug.LogError("Error test");

            Debug.LogWarning("warning\nTest");
            
            //TestInstance();
            //TestInstance();

            
            //GameObject.Find("Player").transform.GetChild(0).GetComponent("MeshRenderer").sharedMaterial.color = Color.black;
            //GameObject.Find("Player").transform.localRotation = Quaternion.Euler(0, 0, 90);
            //Camera.main.transform.position = new Vector3(2,2,-5);

            //string variables = string.Join(",", scope.GetVariableNames().ToArray());
            //Debug.Log("variables: " + variables);
        }

        void TestInstance() {
            PythonInstance py = new PythonInstance(@"
import UnityEngine
from UnityEngine import *

class PyClass:
    index = 1

    def __init__(self):
        pass

    def somemethod(self):
        self.index += 1
        Camera.main.transform.position = Vector3(2,2,-5);
        Debug.Log(""in some method"" + str(self.index))

    def isodd(self, n):
        return 1 == n % 2
");

            py.CallMethod("somemethod");
            py.CallMethod("somemethod");
            py.CallMethod("somemethod");
            py.CallMethod("somemethod");
            Debug.Log(py.CallFunction("isodd", 6));
        }

#region Web GetText
        // doit use with web
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
#endregion
    }
}
