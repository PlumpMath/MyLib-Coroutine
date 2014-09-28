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
	}
}
