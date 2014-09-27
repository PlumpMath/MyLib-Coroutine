using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

public class Main : MonoBehaviour
{
	// キーがnullになることがある、かつ登録した順序を保持したいので型はこうなる。
	private static Dictionary<MyLib.MonoBehaviour, MyLib.BehaviourData> behaviourDict = 
		new Dictionary<MyLib.MonoBehaviour, MyLib.BehaviourData>();

	public static void AddMonoBehaviour(MyLib.MonoBehaviour behaviour)
	{
		if (!behaviourDict.ContainsKey(behaviour))
		{
			behaviourDict.Add(behaviour, new MyLib.BehaviourData(behaviour));
		}
	}

	/// <summary>
	/// このメソッドの実行中に StartCoroutine() が呼ばれると再入するので注意。
	/// </summary>
	/// <returns>The routine.</returns>
	/// <param name="behaviour">Behaviour.</param>
	/// <param name="methodName">Method name.</param>
	/// <param name="routine">Routine.</param>
	public static void AddRoutine(MyLib.MonoBehaviour behaviour, IEnumerator routine)
	{
		MyLib.BehaviourData bdata;

		if (behaviourDict.TryGetValue(behaviour, out bdata))
		{
			if (!routine.MoveNext())
			{
				// すでに終わっているイテレータが渡されたら何もせずに帰る
				return;
			}
			bdata.routineList.AddLast(routine);
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

		// すべてのMonoBehaviourが持つコルーチンを実行。
		// コルーチンは Update の後に呼ばれるので、ここで実行。
		foreach (MyLib.BehaviourData bdata in behaviourDict.Values)
		{
			LinkedListNode<IEnumerator> node = bdata.routineList.First;
			while (node != null)
			{
				var currentNode = node;
				node = node.Next;

				IEnumerator routine = currentNode.Value;
				if (!routine.MoveNext())
				{
					// 終了したイテレータはリストから除去
					bdata.routineList.Remove(currentNode);
				}
			}
		}
	}
}
