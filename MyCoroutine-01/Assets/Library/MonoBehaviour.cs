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

		public void StartCoroutine(IEnumerator routine)
		{
			StartCoroutineCommon(null, routine);
		}

		public void StartCoroutine(string methodName, object arg = null)
		{
			object[] param = (arg == null) ?
				null :
				new object[] { arg };

			IEnumerator routine = (IEnumerator)this.GetType().InvokeMember(
				methodName,
				BindingFlags.InvokeMethod | BindingFlags.Instance | BindingFlags.NonPublic,
				null, this, param
			);

			StartCoroutineCommon(methodName, routine);
		}

		private void StartCoroutineCommon(string methodName, IEnumerator routine)
		{
			// コルーチンの初回実行はStartCoroutineを呼び出したシーケンスで行われるので、
			// このあたりで一回 MoveNext() を呼びたいところ。
			Main.AddRoutine(this, routine);
		}
	}
}