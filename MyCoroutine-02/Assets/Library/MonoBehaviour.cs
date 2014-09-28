using System.Collections;
using System.Reflection;

namespace MyLib
{
	public class MonoBehaviour
	{
		public virtual void Start()
		{
		}

		public virtual void Update()
		{
		}

		public MyLib.Coroutine StartCoroutine(IEnumerator routine)
		{
			return StartCoroutineCommon(null, routine);
		}

		public MyLib.Coroutine StartCoroutine(string methodName, object arg = null)
		{
			object[] param = (arg == null) ?
				null :
				new object[] { arg };

			IEnumerator routine = (IEnumerator)this.GetType().InvokeMember(
				methodName,
				BindingFlags.InvokeMethod | BindingFlags.Instance | BindingFlags.NonPublic,
				null, this, param
			);

			return StartCoroutineCommon(methodName, routine);
		}

		private MyLib.Coroutine StartCoroutineCommon(string methodName, IEnumerator routine)
		{
			return Main.AddRoutine(this, methodName, routine);
		}

		public void StopCoroutine(string methodName)
		{
			Main.RemoveRoutine(this, methodName);
		}

		public void StopAllCoroutines()
		{
			Main.RemoveAllRoutines(this);
		}
	}
}