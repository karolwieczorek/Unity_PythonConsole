using System;
using UnityEngine;
using UnityEngine.UI;
namespace UnityPythonConsole.Assets.Code {
    [RequireComponent(typeof(InputField))]
    public class PyInputField : MonoBehaviour {
        InputField inputField;

        [SerializeField] Button button;
        PyConsole pyConsole;
        private Action<string> executeAction;

        private void Awake() {
            inputField = GetComponent<InputField>();

            inputField.onValueChanged.AddListener(InputValueChanged);
            button.onClick.AddListener(ButtonClick);
        }

        private void InputValueChanged(string arg0) {
            // doit alternative place to reset log history index (look on Update -> GetKeyDown(UpArrow))
        }

        internal void Init(PyConsole pyConsole) {
            this.pyConsole = pyConsole;
        }

        internal void SetExecuteAction(Action<string> executeInputCommand) {
            this.executeAction = executeInputCommand;
        }

        private void ButtonClick() {
            ExecuteInputCommand();
        }

        void ExecuteInputCommand() {
            var text = inputField.text;
            if (string.IsNullOrEmpty(text))
                return;

            pyConsole.AddExpressionLog(text);

            executeAction(text);

            inputField.text = "";
        }

        private void Update() {
            if (inputField.isFocused) {
                if ((Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)) 
                    && Input.GetKey(KeyCode.LeftShift) == false) 
                {
                    ExecuteInputCommand();
                }
                
                if (Input.anyKeyDown || Input.anyKey) {
                    if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.UpArrow)) {
                        inputField.text = pyConsole.GetPrevCommand(); // create and use log history index
                        inputField.MoveTextEnd(true);
                    } else {
                        // doit reset log history index
                    }
                }
            }
        }
    }
}