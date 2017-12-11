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

        button.onClick.AddListener(ButtonClick);
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
        pyConsole.AddExpressionLog(text);

        executeAction(text);
    }

    private void Update() {
        if (inputField.isFocused) {
            if (Input.GetKeyDown(KeyCode.Return) && Input.GetKey(KeyCode.LeftShift) == false) {
                ExecuteInputCommand();
                inputField.text = "";
            }
        }
    }
}