using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class Location
{
	[HideInInspector]
	protected Position[] PlayerPositions;

	[HideInInspector]
	protected Position[] EnemyPositions;

	[SerializeField]
	private int _playerPositions;

	[SerializeField]
	private int _enemyPositions;

	[SerializeField]
	private List<ItemCard> ItemCards;

	#region Set Up

	private void Awake()
	{
		InitializePositions();
	}

	/// <summary>
	/// Initializes the positions arrays.
	/// </summary>
	public void InitializePositions()
	{
		PlayerPositions = new Position[_playerPositions];

		EnemyPositions = new Position[_enemyPositions];
	}

	#endregion

	#region Card Manipulation

	/// <summary>
	/// Draws the top item card.
	/// </summary>
	/// <returns>The item card.</returns>
	public Card DrawItemCard()
	{
		var returnItem = ItemCards [0];
		ItemCards.RemoveAt(0);
		return returnItem;
	}

	/// <summary>
	/// Shuffles the item deck.
	/// </summary>
	public void ShuffleDeck()
	{
		ItemCards.Shuffle();
	}

	/// <summary>
	/// Places the card on the bottom of the deck.
	/// </summary>
	/// <param name="cardToAdd">Card to add to the bottom of the deck.</param>
	public void PlaceCardOnBottom(ItemCard cardToAdd)
	{
		ItemCards.Add(cardToAdd);
	}

	/// <summary>
	/// Places the card on top of the deck.
	/// </summary>
	/// <param name="cardToAdd">Card to add to the top of the deck.</param>
	public void PlaceCardOnTop(ItemCard cardToAdd)
	{
		ItemCards.Insert(0, cardToAdd);
	}

	#endregion

	#region Piece Adding

	/// <summary>
	/// Adds the player piece to the location, if applicaple.
	/// </summary>
	/// <returns><c>true</c>, if player piece was added, <c>false</c> otherwise.</returns>
	/// <param name="newPiece">New piece to add to this location.</param>
	public bool AddPlayerPiece(PlayerPiece newPiece)
	{
		return AddPiece(newPiece, PlayerPositions);
	}

	/// <summary>
	/// Adds the zombie piece to the zombie positions.
	/// </summary>
	/// <returns><c>true</c>, if zombie piece was added, <c>false</c> otherwise.</returns>
	/// <param name="zombiePiece">Zombie piece to add to the enemy positions.</param>
	public bool AddZombiePiece(EnemyPiece zombiePiece)
	{
		return AddPiece(zombiePiece, EnemyPositions);
	}

	/// <summary>
	/// Adds a piece to the passed positions array.
	/// </summary>
	/// <returns><c>true</c>, if piece was added, <c>false</c> otherwise.</returns>
	/// <param name="pieceToAdd">Piece to add to the passed positions.</param>
	/// <param name="positionsToCheck">Positions to check for adding too.</param>
	private bool AddPiece(LocationPiece pieceToAdd, Position[] positionsToCheck)
	{
		// For each position in the PlayerPositions Array...
		for (int i = 0; i < positionsToCheck.Length; i++)
		{

			// ...If adding the character returns true, meaning it was added...
			if (positionsToCheck [i].AddCharacter(pieceToAdd))
			{

				// ...return true.
				return true;
			}
		}

		return false;
	}

	#endregion

	#region Piece Removal

	/// <summary>
	/// Removes the survivor piece passed from the player positions.
	/// </summary>
	/// <param name="survivorToRemove">Survivor to remove from player positions.</param>
	/// <returns><c>true</c>, if survivor piece was removed, <c>false</c> otherwise.</returns>
	public bool RemoveSurvivorPiece(PlayerPiece survivorToRemove)
	{
		return RemovePiece(survivorToRemove, PlayerPositions);
	}

	/// <summary>
	/// Removes the zombie piece passed from the zombie positions.
	/// </summary>
	/// <param name="zombieToRemove">Zombie to remove from enemy positions.</param>
	/// <returns><c>true</c>, if zombie piece was removed, <c>false</c> otherwise.</returns>
	public bool RemoveZombiePiece(EnemyPiece zombieToRemove)
	{
		return RemovePiece(zombieToRemove, EnemyPositions);
	}

	/// <summary>
	/// Removes a zombie piece from the enemy position queue.
	/// </summary>
	/// <returns><c>true</c>, if zombie piece was removed, <c>false</c> otherwise.</returns>
	public bool RemoveZombiePiece()
	{
		// For each enemy in enemy positions, starting at the front of the queue...
		for (int i = EnemyPositions.Length - 1; i >= 0; i--)
		{

			// ...If the removeal results in a non null return, meaning something was removed...
			if (EnemyPositions [i].RemoveCharacter() != null)
			{

				// ...Then return true.
				return true;
			}
		}

		return false;
	}

	/// <summary>
	/// Removes the piece passed from the passed Position Array.
	/// </summary>
	/// <returns><c>true</c>, if piece was removed, <c>false</c> otherwise.</returns>
	/// <param name="pieceToRemove">Piece to remove from the passed array.</param>
	/// <param name="positionsToCheck">Positions to check for the piece in, and remove from.</param>
	private bool RemovePiece(LocationPiece pieceToRemove, Position[] positionsToCheck)
	{
		// For each piece in the passed piece collection...
		for (int i = 0; i < positionsToCheck.Length; i++)
		{

			// ...If the piece removal resulted in a non null result, meaning something was removed...
			if (positionsToCheck [i].RemoveCharacter(pieceToRemove) != null)
			{

				// ...Then return true.
				return true;
			}
		}

		return false;
	}

	#endregion

	#region Influence Searches

	/// <summary>
	/// Finds the lowest infulence survivor in the location.
	/// </summary>
	/// <returns>The lowest infulence survivor.</returns>
	public PlayerPiece FindLowestInfulenceSurvivor()
	{
		return FindLowestInfulenceSurvivor(new PlayerPiece[0]);
	}

	/// <summary>
	/// Finds the lowest infulence survivor in the location.
	/// </summary>
	/// <returns>The lowest infulence survivor.</returns>
	/// <param name="excludingList">Excluding list to not check for in the search.</param>
	public PlayerPiece FindLowestInfulenceSurvivor(PlayerPiece[] excludingList)
	{
		PlayerPiece lowestInfulenceCharacter = (PlayerPiece)PlayerPositions [0].GetOccupant();

		// For each player in the player positions...
		for (int i = 0; i < PlayerPositions.Length; i++)
		{
			// ...Get the current player...
			var currentPlayer = (PlayerPiece)PlayerPositions [i].GetOccupant();

			if (currentPlayer == null)
				continue;

			// ...And reset the isExcluded flag...
			var isExcluded = false;

			// ...And for each player in the exluding list...
			for (int x = 0; x < excludingList.Length; x++)
			{
				// ..If the player we are currently checking is in the exluding list...
				if (currentPlayer.Equals(excludingList [x]))
				{
					// ...Flag this member as excluded.
					isExcluded = true;
					break;
				}
			}

			// ...If the isExcluded attribute is flagged...
			if (isExcluded)
			{
				// ....Then skip this entry.
				continue;
			}

			if (currentPlayer.Infulence < lowestInfulenceCharacter.Infulence)
				lowestInfulenceCharacter = currentPlayer;
		}

		return lowestInfulenceCharacter;
	}

	#endregion


}
