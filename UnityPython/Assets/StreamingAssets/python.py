import UnityEngine
from UnityEngine import *

Debug.Log("Hello From IronPython")
counter = 25 + 11
timeBackup = Time.deltaTime;


class PyClass:
    def __init__(self):
        pass

    def somemethod(self):
        return 'in some method'

    def isodd(self, n):
        return 1 == n % 2


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


print "Hello, World!"
