﻿public class Test : MyLib.MonoBehaviour
{
	public override void Start()
	{
		UnityEngine.Debug.Log("Start()");
	}

	private int updateCount;

	public override void Update()
	{
		if (updateCount < 50)
		{
			UnityEngine.Debug.Log("Update():" + updateCount);
			++updateCount;
		}
	}
}