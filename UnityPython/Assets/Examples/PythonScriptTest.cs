using IronPython.Hosting;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace UnityPython.Assets.Examples {
    public class PythonScriptTest : MonoBehaviour {

        [SerializeField] InputField inputField;
        [SerializeField] Button button;

        Microsoft.Scripting.Hosting.ScriptEngine engine;
        Microsoft.Scripting.Hosting.ScriptScope scope;

        string pythonScript;
        private void Awake() {
            pythonScript = GetText();
            button.onClick.AddListener(ButtonClick);
        }

        private void ButtonClick() {
            ExecuteInputCommand();
        }

        private void ExecuteInputCommand() {
            engine.Execute(inputField.text, scope);
        }

        private void Start() {
            engine = Python.CreateEngine();
            engine.Runtime.LoadAssembly(typeof(GameObject).Assembly);

            engine.Runtime.LoadAssembly(typeof(Image).Assembly);
            //engine.ImportModule("");
            scope = engine.CreateScope();

            engine.Execute(pythonScript, scope);
            //var scope = engine.ExecuteFile(path);
            int counter = scope.GetVariable<int>("counter");
            Debug.Log(counter);

            float timeBackup = scope.GetVariable<float>("timeBackup");
            Debug.Log(timeBackup);

            Debug.LogError("WTF\nwtf\nlol\nxD");

            Debug.LogWarning("warning\nwarning\nwarning");

            TestInstance();
            TestInstance();

            
            //GameObject.Find("Player").transform.GetChild(0).GetComponent("MeshRenderer").sharedMaterial.color = Color.black;
            //GameObject.Find("Player").transform.localRotation = Quaternion.Euler(0, 0, 90);
            //Camera.main.transform.position = new Vector3(2,2,-5);

            string variables = string.Join(",", scope.GetVariableNames().ToArray());
            Debug.Log("variables: " + variables);
        }

        private void Update() {
            if (inputField.isFocused) {
                if (Input.GetKeyDown(KeyCode.Return) && Input.GetKey(KeyCode.LeftShift) == false) {
                    ExecuteInputCommand();
                    inputField.text = "";
                }
            }
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

        string GetText() {
            string filePath = System.IO.Path.Combine(Application.streamingAssetsPath, "python.py");
            return System.IO.File.ReadAllText(filePath);
        }

#region Web GetText
        // doit use with web
        IEnumerator GetPythonScript() {
            string filePath = System.IO.Path.Combine(Application.streamingAssetsPath, "python.py");

            string result;
            if (filePath.Contains("://") || filePath.Contains(":///")) {
                WWW www = new WWW(filePath);
                yield return www;
                result = www.text;
            } else
                result = System.IO.File.ReadAllText(filePath);

            pythonScript = result;
        }
#endregion
    }
}
