using System.Collections;
using System.Collections.Generic;

namespace MyLib
{
	public class Coroutine
	{
		public string methodName;
		public IEnumerator routine;

		public Coroutine(string methodName, IEnumerator routine)
		{
			this.methodName = methodName;
			this.routine = routine;
		}
	}
}