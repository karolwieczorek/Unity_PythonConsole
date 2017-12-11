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