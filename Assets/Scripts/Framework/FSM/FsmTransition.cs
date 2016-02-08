using UnityEngine;
using System.Collections;

public abstract class FsmTransition<TStateEnum>
{
	public TStateEnum DestinationState;

	public abstract void OnTransition();

}
