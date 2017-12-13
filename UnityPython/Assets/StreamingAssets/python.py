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



triangle = GameObject.Find("Triangle")
moveSpeed = 1

def Movement(*args):
    if Input.GetKey(KeyCode.LeftArrow):
        triangle.transform.position += Vector3.left * Time.deltaTime * moveSpeed

    if Input.GetKey(KeyCode.RightArrow):
        triangle.transform.position += Vector3.right * Time.deltaTime * moveSpeed


unityEvents.update += Movement


circle = GameObject.Find("Circle")
circles = [circle]
ballSpeed = 5
ballCurrentSpeed = 0.1

def Projectile(*args):
    if ballCurrentSpeed > 0:
        for circle in circles:
            circle.transform.position += Vector3(0, Time.deltaTime * ballCurrentSpeed, 0)
            global ballCurrentSpeed
        #ballCurrentSpeed -= .1

unityEvents.update += Projectile

def Shoot(*args):
    if Input.GetKeyDown(KeyCode.X):
        newCircle = unityEvents.Instantiate(circle)
        newCircle.transform.position = triangle.transform.position + Vector3.up
        circles.append(newCircle)
        global ballCurrentSpeed
        ballCurrentSpeed = ballSpeed
        newCircle.GetComponent("SpriteRenderer").color = Random.ColorHSV()

unityEvents.update += Shoot

def NewBall(*args):
    unityEvents.Instantiate(circle)