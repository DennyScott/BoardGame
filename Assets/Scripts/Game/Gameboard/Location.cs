using UnityEngine;
using System;

[Serializable]
public class Location
{
	[HideInInspector]
	public Position[] PlayerPositions;

	[HideInInspector]
	public Position[] EnemyPositions;

	[SerializeField]
	private int _playerPositions;

	[SerializeField]
	private int _enemyPositions;

	public void InitializePositions ()
	{
		PlayerPositions = new Position[_playerPositions];

		EnemyPositions = new Position[_enemyPositions];
	}
}
