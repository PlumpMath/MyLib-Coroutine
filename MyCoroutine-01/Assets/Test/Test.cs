using MyLib;
using System.Collections;

public class Test : MonoBehaviour
{
	public override void Start()
	{
		StartCoroutine("RoutineTest", "A");
		StartCoroutine("RoutineTest", "B");
	}

	int updateCount;

	public override void Update()
	{
		UnityEngine.Debug.Log("    Update:" + updateCount);
		++updateCount;
	}

	private IEnumerator RoutineTest(string name)
	{
		int count = 0;

		while (true)
		{
			if (count >= 100)
			{
				yield break;
			}

			++count;
			UnityEngine.Debug.Log("★" + name + ", " + count);
			yield return null;
		}
	}
}