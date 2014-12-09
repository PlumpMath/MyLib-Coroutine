using MyLib;
using System.Collections;

public class Test : MonoBehaviour
{
	public override void Start()
	{
		StartCoroutine(RoutineTest("A"));
		StartCoroutine("RoutineTest", "B");
	}

	private IEnumerator RoutineTest(string name)
	{
		int count = 0;

		while (true)
		{
			++count;

			if (count == 5)
			{
				StopCoroutine("RoutineTest");
			}
			if (count == 10)
			{
				yield break;
			}

			UnityEngine.Debug.Log(">>> " + name + ", " + count);
			yield return null;
		}
	}
}