using System.Collections;
using System.Collections.Generic;

public class Main : UnityEngine.MonoBehaviour
{
	private static Dictionary<MyLib.MonoBehaviour, MyLib.BehaviourData> behaviourDict = 
		new Dictionary<MyLib.MonoBehaviour, MyLib.BehaviourData>();

	public static void AddMonoBehaviour(MyLib.MonoBehaviour behaviour)
	{
		if (!behaviourDict.ContainsKey(behaviour))
		{
			behaviourDict.Add(behaviour, new MyLib.BehaviourData(behaviour));
		}
	}

	public static MyLib.Coroutine AddRoutine(MyLib.MonoBehaviour behaviour, string methodName, IEnumerator routine)
	{
		MyLib.BehaviourData bdata;

		if (behaviourDict.TryGetValue(behaviour, out bdata))
		{
			var coroutine = new MyLib.Coroutine(methodName, routine);
			
			// ひとまず1回実行
			routine.MoveNext();
			bdata.routineList.AddLast(coroutine);
			return coroutine;
		}
		else
		{
			// ここに来ることはない
			return null;
		}
	}

	public static void RemoveRoutine(MyLib.MonoBehaviour behaviour, string methodName)
	{
		MyLib.BehaviourData bdata;
		if (behaviourDict.TryGetValue(behaviour, out bdata))
		{
			LinkedListNode<MyLib.Coroutine> node = bdata.routineList.First;
			while (node != null)
			{
				var oldNode = node;
				node = node.Next;
				if (oldNode.Value.methodName == methodName)
				{
					bdata.routineList.Remove(oldNode);
				}
			}
		}
	}

	public static void RemoveAllRoutines(MyLib.MonoBehaviour behaviour)
	{
		MyLib.BehaviourData bdata;
		if (behaviourDict.TryGetValue(behaviour, out bdata))
		{
			bdata.routineList.Clear();
		}
	}

	public void Awake()
	{
		AddMonoBehaviour(new Test());
	}

	public void Update()
	{
		// すべてのMonoBehaviourを実行
		foreach (MyLib.BehaviourData bdata in behaviourDict.Values)
		{
			if (!bdata.mainloopBegan)
			{
				bdata.behaviour.Start();
				bdata.mainloopBegan = true;
			}

			bdata.behaviour.Update();
		}

		foreach (MyLib.BehaviourData bdata in behaviourDict.Values)
		{
			LinkedListNode<MyLib.Coroutine> node = bdata.routineList.First;
			while (node != null)
			{
				MyLib.Coroutine coroutine = node.Value;
				if (!coroutine.routine.MoveNext())
				{
					var currentNode = node;
					node = node.Next;
					bdata.routineList.Remove(currentNode);
				}
				else
				{
					node = node.Next;
				}
			}
		}
	}
}
