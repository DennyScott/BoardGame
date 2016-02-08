using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Finite State Machine that contains numerous states and is controlled through the use of transitions.  Should use Enum's for State keys, Enums for Transition keys, and a Concrete State pattern for States
/// </summary>
public class Fsm<TStateEnum, TStateAction, TConcreteState> where TConcreteState : IFsmState
{
	#region Private Variables

	//A Dictionary of keys that will be enums and values that will be a concrete state pattern.
	private readonly Dictionary<TStateEnum, TConcreteState> _states = new Dictionary<TStateEnum, TConcreteState>();

	// A Dictionary of keys that will be state enums and values that will be dictionaries, lets call them DictionaryB's.
	// Each DictionaryB's will have key that will be Transition enums, and will have a value of class inheriting from the abstract
	// class FsmTransition. FsmTransition must also contain refrence to the StateEnum, as it will contiain the key of the state to
	// exit to.
	private Dictionary<TStateEnum, Dictionary<TStateAction, FsmTransition<TStateEnum>>> _transitions = new Dictionary<TStateEnum, Dictionary<TStateAction, FsmTransition<TStateEnum>>>();

	#endregion

	#region Auto Properties

	public TConcreteState CurrentState
	{ get; private set; }

	public TStateEnum CurrentStateName
	{ get; private set; }

	public int Count { get { return _states.Count; } }

	#endregion

	#region State Methods

	/// <summary>
	/// Adds a State to the State Machine
	/// </summary>
	/// <param name="key">The key for this state</param>
	/// <param name="state">The State for this key</param>
	public void AddState(TStateEnum key, TConcreteState state)
	{
		_states.Add(key, state);
		_transitions.Add(key, new Dictionary<TStateAction, FsmTransition<TStateEnum>>());
	}

	/// <summary>
	/// Removes the state from the state machine with the given key
	/// </summary>
	/// <param name="key">The key to search for to remove</param>
	public void RemoveState(TStateEnum key)
	{
		_states.Remove(key);
	}

	[Obsolete("SetCurrentState is depricated.  Please use TriggerTransition instead")]
	/// <summary>
	/// Sets the current state to the state with the corresponding key
	/// </summary>
	/// <param name="key">The key to search for</param>
	public void SetCurrentState(TStateEnum key)
	{
		//If the fsm does not contain this key
		if(!_states.ContainsKey(key))
		{
			Debug.LogError("State Machine Does not contian the key: " + key);
			return;
		}

		//If this is not the first state added
		if(CurrentState != null)
		{
			CurrentState.OnExit();
		}

		CurrentState = _states[key];
		CurrentStateName = key;
		CurrentState.OnEntry();
	}

	[Obsolete("SetCurrentStateIf is depricated.  Please use TriggerTransition instead")]
	/// <summary>
	/// Sets the current state to the state with the corresponding state if chekFunc returns true
	/// </summary>
	/// <param name="key">The key to search for</param>
	/// <param name="checkFunc">The function that will run and if true, changes the state</param>
	public void SetCurrentStateIf(TStateEnum key, Func<bool> checkFunc)
	{
		if(checkFunc())
		{
			SetCurrentState(key);
		}
	}

	[Obsolete("SetCurrentStateIf is depricated.  Please use TriggerTransition instead")]
	/// <summary>
	/// Sets the current state to the state with the corresponding state if checkBool returns true
	/// </summary>
	/// <param name="key">The key to search for</param>
	/// <param name="checkBool">If true, allows the state to change</param>
	public void SetCurrentStateIf(TStateEnum key, bool checkBool)
	{
		if(checkBool)
		{
			SetCurrentState(key);
		}
	}


	/// <summary>
	/// Sets the state of the state machine to the passed state.
	/// </summary>
	/// <param name="key">Key.</param>
	private void SetState(TStateEnum key)
	{

		// If the fsm does not contain this key...
		if(!_states.ContainsKey(key))
		{
			// ...Then log an error to the developer...
			Debug.LogError("State Machine - Does not contian the key: " + key);

			// ...And return out of the SetState method
			return;
		}

		// The current State gets set to the new state
		CurrentState = _states[key];

		// Set the State name to the passed key
		CurrentStateName = key;

		// And call that states OnEntry method
		CurrentState.OnEntry();
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
	/// <param name="prevState">The previous state to transition from</param>
	/// <param name="destinationState">The state to transition to</param>
	/// <param name="actionToPerform">The action to perform if there is a transition to the destination state from the current</param>
	public void AddTransition(TStateEnum prevState, TStateEnum destinationState, TStateAction StateAction, FsmTransition<TStateEnum> transition)
	{
		//Get the transitions for the prevState
		var cStateTrans = _transitions[prevState];

		//If the Transition Dictionary does not have the current state's transitions...
		if(cStateTrans == null)
		{
			// ...Then log an error to inform a developer...
			Debug.LogError("Current State for transition not found");

			// ...and exit out of the add transition method.
			return;
		}
			
		// If the Transition Dictionary does not contain the passed Transition Key...
		if(!cStateTrans.ContainsKey(StateAction))
		{
			// ...add it to the transition Dictionary, as well as the new transition.
			cStateTrans.Add(StateAction, transition);
		} else
		{
			// ...else Log a warning to the developer that it already exists...
			Debug.LogWarning("Transition: " + StateAction + " - Overwritten");

			// ...and override it with the new transition.
			cStateTrans[StateAction] = transition;
		}
	}


	/// <summary>
	/// Triggers the transition with the matching key.  Performs the onExit, then the transition, and then enters the new state.
	/// </summary>
	/// <param name="key">Key of the transition to perform.</param>
	public void TriggerTransition(TStateAction key)
	{

		//If the current state is null, meaning there is no state set yet...
		if(CurrentState == null)
		{
			// ...Then log an error to the developer...
			Debug.LogError("Current State has not yet been set");

			// ...and return out of the trigger tranition method.
			return;
		}
			
		// Get the transitions for this current state.
		var cStateTrans = _transitions[CurrentStateName];

		// If the transitions for this state do not exists...
		if(cStateTrans == null)
		{
			// ...Then log an error to the developer...
			Debug.LogError("Current State for transition not found");

			// ...and return out of the trigger transition method.
			return;
		}

		// Get the transition with the given key, ex. MoveFaster
		var transition = cStateTrans[key];

		// If the transition does not exist...
		if(transition == null)
		{
			// ...Then log a warning to the developer that the transition is not attached to this state...
			Debug.LogWarning("Current State does not have transition: " + key);

			// ...And return out of the Trigger Transition method.
			return;
		}

		// Call the current states On Exit.
		CurrentState.OnExit();

		// Then call the found transition's On Transition.
		transition.OnTransition();

		//And then Switch the current state to the state attached to the end of the Transition
		SetState(transition.DestinationState);
	}

	#endregion
}
