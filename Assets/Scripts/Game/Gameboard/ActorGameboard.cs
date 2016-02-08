using UnityEngine;
using System.Collections;

public class ActorGameboard : MonoBehaviour
{
	[SerializeField, Tooltip ("The gameboard that this games take place on")]
	private Gameboard _gameboard;

	[SerializeField]
	private Location _gasStation;

	[SerializeField]
	private Location _library;

	[SerializeField]
	private Location _policeStation;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
