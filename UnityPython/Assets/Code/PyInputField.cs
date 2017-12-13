using System;
using UnityEngine;
using UnityEngine.UI;

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
        //if (arg0 != "")
        //    Debug.Log(arg0);
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
        //if (text.EndsWith("\n"))
        //    text = text.Remove(text.Length - 2, 2);

        pyConsole.AddExpressionLog(text);

        executeAction(text);
        
        inputField.text = "";
    }

    private void Update() {
        if (inputField.isFocused) {
            if (Input.GetKeyDown(KeyCode.Return) && Input.GetKey(KeyCode.LeftShift) == false) {
                ExecuteInputCommand();
            }

            if (inputField.text == "" && Input.GetKeyDown(KeyCode.UpArrow)) {
                inputField.text = pyConsole.GetPrevCommand();
                inputField.MoveTextEnd(true);
            }
        }
    }
}