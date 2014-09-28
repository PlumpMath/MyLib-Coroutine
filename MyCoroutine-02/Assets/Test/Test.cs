using MyLib;
using System.Collections;

public class Test : MonoBehaviour
{
	public override void Start()
	{
		// イテレータ指定で起動
		StartCoroutine(RoutineTest("A"));
		// メソッド名指定で起動
		StartCoroutine("RoutineTest", "B");
	}

	int updateCount;

	public override void Update()
	{
		UnityEngine.Debug.Log("Update:" + updateCount);
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