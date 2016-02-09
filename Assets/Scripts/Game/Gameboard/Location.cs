using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class Location
{

	protected PositionSet<EnemyPiece> EnemyPositions;

	protected PlayerPositionSet PlayerPositions;

	[SerializeField]
	private int _playerPositions;

	[SerializeField]
	private int _enemyPositions;

	[SerializeField]
	private List<ItemCard> ItemCards;

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
		return PlayerPositions.Queue(newPiece);
	}

	/// <summary>
	/// Adds the zombie piece to the zombie positions.
	/// </summary>
	/// <returns><c>true</c>, if zombie piece was added, <c>false</c> otherwise.</returns>
	/// <param name="zombiePiece">Zombie piece to add to the enemy positions.</param>
	public bool AddZombiePiece(EnemyPiece zombiePiece)
	{
		return EnemyPositions.Queue(zombiePiece);
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
		return  PlayerPositions.Remove(survivorToRemove) != null;
	}

	/// <summary>
	/// Removes the zombie piece passed from the zombie positions.
	/// </summary>
	/// <param name="zombieToRemove">Zombie to remove from enemy positions.</param>
	/// <returns><c>true</c>, if zombie piece was removed, <c>false</c> otherwise.</returns>
	public bool RemoveZombiePiece(EnemyPiece zombieToRemove)
	{
		return EnemyPositions.Remove(zombieToRemove) != null;
	}

	/// <summary>
	/// Removes a zombie piece from the enemy position queue.
	/// </summary>
	/// <returns><c>true</c>, if zombie piece was removed, <c>false</c> otherwise.</returns>
	public bool RemoveZombiePiece()
	{
		return EnemyPositions.Pop() != null;
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
		return PlayerPositions.FindWithLowestInfluence(excludingList);
	}

	public PlayerPiece FindHighestInfulenceSurvivor()
	{
		return FindHighestInfulenceSurvivor(new PlayerPiece[0]);
	}

	public PlayerPiece FindHighestInfulenceSurvivor(PlayerPiece[] excludingList)
	{
		return PlayerPositions.FindWithHighestInfluence(excludingList);
	}

	#endregion


}
