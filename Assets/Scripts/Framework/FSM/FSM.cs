using System;
using System.Collections.Generic;
using UnityEngine;

public class Fsm<TEnum, T> where T : IFsmState
{
	#region Private Variables

	private readonly Dictionary<TEnum, T> _states = new Dictionary<TEnum, T>();
	private Dictionary<TEnum, Dictionary<TEnum, Action>> _transitions = new Dictionary<TEnum, Dictionary<TEnum, Action>>();

	#endregion

	#region Auto Properties

	public T CurrentState
	{ get; private set; }

	public TEnum CurrentStateName
	{ get; private set; }

	public int Count { get { return _states.Count; } }

	#endregion

	#region State Methods

	/// <summary>
	/// Adds a State to the State Machine
	/// </summary>
	/// <param name="key">The key for this state</param>
	/// <param name="state">The State for this key</param>
	public void AddState(TEnum key, T state)
	{
		_states.Add(key, state);
		_transitions.Add(key, new Dictionary<TEnum, Action>());
	}

	/// <summary>
	/// Removes the state from the state machine with the given key
	/// </summary>
	/// <param name="key">The key to search for to remove</param>
	public void RemoveState(TEnum key)
	{
		_states.Remove(key);
	}

	/// <summary>
	/// Sets the current state to the state with the corresponding key
	/// </summary>
	/// <param name="key">The key to search for</param>
	public void SetCurrentState(TEnum key)
	{
		//If the fsm does not contain this key
		if(!_states.ContainsKey(key))
		{
			Debug.LogError("State Machine Does not contian the key: " + key);
			return;
		}

		//If this is the first state added
		if(CurrentState != null)
		{
			CurrentState.OnExit();
			if(_transitions[CurrentStateName].ContainsKey(key))
			{
				_transitions[CurrentStateName][key]();
			}
		}

		CurrentState = _states[key];
		CurrentStateName = key;
		CurrentState.OnEntry();
	}

	/// <summary>
	/// Sets the current state to the state with the corresponding state if chekFunc returns true
	/// </summary>
	/// <param name="key">The key to search for</param>
	/// <param name="checkFunc">The function that will run and if true, changes the state</param>
	public void SetCurrentStateIf(TEnum key, Func<bool> checkFunc)
	{
		if(checkFunc())
		{
			SetCurrentState(key);
		}
	}

	/// <summary>
	/// Sets the current state to the state with the corresponding state if checkBool returns true
	/// </summary>
	/// <param name="key">The key to search for</param>
	/// <param name="checkBool">If true, allows the state to change</param>
	public void SetCurrentStateIf(TEnum key, bool checkBool)
	{
		if(checkBool)
		{
			SetCurrentState(key);
		}
	}

	/// <summary>
	/// Clears the State Machine
	/// </summary>
	public void Clear()
	{
		_states.Clear();
		_transitions.Clear();
	}

	#endregion

	#region Transition Methods

	/// <summary>
	/// Adds a transition between the two passed states
	/// </summary>
	/// <param name="currentState">The current State to transition from</param>
	/// <param name="destinationState">The state to transition to</param>
	/// <param name="actionToPerform">The action to perform if there is a transition to the destination state from the current</param>
	public void AddTransition(TEnum currentState, TEnum destinationState, Action actionToPerform)
	{
		var cStateTrans = _transitions[currentState];
		if(cStateTrans == null)
		{
			Debug.LogError("Current State for transition not found");
			return;
		}

		cStateTrans[destinationState] = cStateTrans.ContainsKey(destinationState) ? cStateTrans[destinationState] += actionToPerform : actionToPerform;
	}

	#endregion
}
