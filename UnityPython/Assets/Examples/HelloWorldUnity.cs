using UnityEngine;

// source: https://github.com/exodrifter/unity-python

namespace Exodrifter.UnityPython.Examples
{
	public class HelloWorldUnity : MonoBehaviour
	{
		void Start()
		{
            //var engine = global::UnityPython.CreateEngine();
            var engine = IronPython.Hosting.Python.CreateEngine();
            var scope = engine.CreateScope();

			string code = "import UnityEngine\n";
			code += "UnityEngine.Debug.Log('Hello world!')";

			var source = engine.CreateScriptSourceFromString(code);
			source.Execute(scope);
		}
	}
}