using UnityEngine;
using System.Collections;

public class QuickFsmTransition<StateEnums> : FsmTransition<StateEnums>
{

	public QuickFsmTransition (StateEnums endDestination)
	{
		this.DestinationState = endDestination;
	}

	public override void OnTransition()
	{
	}
}
