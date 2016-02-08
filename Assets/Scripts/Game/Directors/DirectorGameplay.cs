using UnityEngine;
using System.Collections;

public class DirectorGameplay : Director<DirectorGameplay>
{

	private int _numberOfPlayers = 2;
	private int _roundsLeft = 5;
	private int _morale = 5;


	private void Start ()
	{
		StartGame ();
	}

	private void StartGame ()
	{
		
	}
}
