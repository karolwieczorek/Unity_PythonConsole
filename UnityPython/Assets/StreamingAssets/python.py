import UnityEngine
from UnityEngine import *

Debug.Log("Hello From IronPython")


# print hooks
# source: https://stackoverflow.com/questions/8288717/python-print-passing-extra-text-to-sys-stdout
import sys

class StdOutHook:
    def write(self, text):
        #sys.__stdout__.write("stdout hook received text: %s\n" % repr(text))
        if text != "\n" and text  != "":
            pyConsole.AddPythonLog(text)

sys.stdout = StdOutHook()

class StdErrHook:
    def write(self, text):
        #sys.__stderr__.write("stderr hook received text: %s\n" % repr(text))
        if text != "\n" and text  != "":
            pyConsole.AddPythonLog(text)


sys.stderr = StdErrHook()


def GetRootObjects():
    return UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects()