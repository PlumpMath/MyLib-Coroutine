namespace MyLib
{
	public class BehaviourData
	{
		public MyLib.MonoBehaviour behaviour;
		public bool mainloopBegan;

		public BehaviourData(MyLib.MonoBehaviour behaviour)
		{
			this.behaviour = behaviour;
			this.mainloopBegan = false;
		}
	}
}