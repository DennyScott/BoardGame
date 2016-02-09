using UnityEngine;
using System;

[Serializable]
public class Position
{
	private LocationPiece _currentOccupant;

	/// <summary>
	/// Gets a value indicating whether this position is occupied.
	/// </summary>
	/// <value><c>true</c> if this position is occupied; otherwise, <c>false</c>.</value>
	public bool IsOccupied { get { return _currentOccupant != null; } }

	/// <summary>
	/// Places the character piece into this position, if available.
	/// </summary>
	/// <returns><c>true</c>, if character was placed, <c>false</c> otherwise.</returns>
	/// <param name="character">Character to place into this position.</param>
	public bool AddCharacter(LocationPiece character)
	{
		if (IsOccupied)
			return false;

		_currentOccupant = character;
		return true;
	}

	/// <summary>
	/// Removes the character in this position.
	/// </summary>
	/// <returns>The character that was in this position.</returns>
	public LocationPiece RemoveCharacter()
	{
		if (!IsOccupied)
			return null;

		var temp = _currentOccupant;
		_currentOccupant = null;
		return temp;
	}

	/// <summary>
	/// Removes the character in this position.
	/// </summary>
	/// <returns>The character that was in this position.</returns>
	public LocationPiece RemoveCharacter(LocationPiece characterToCheck)
	{
		if (_currentOccupant.Equals(characterToCheck))
			return RemoveCharacter();

		return null;
	}

	/// <summary>
	/// Gets the occupant of this position.
	/// </summary>
	/// <returns>The occupant in this position.</returns>
	public LocationPiece GetOccupant()
	{
		return _currentOccupant;
	}
}
