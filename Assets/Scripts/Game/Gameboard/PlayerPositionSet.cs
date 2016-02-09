using UnityEngine;
using System.Collections;

public class PlayerPositionSet : PositionSet<PlayerPiece>
{
	public PlayerPiece FindWithHighestInfluence(PlayerPiece[] excludingList)
	{
		return FindSurvivorWithComparator(excludingList, LowestInfluenceComparator);
	}

	public PlayerPiece FindWithLowestInfluence(PlayerPiece[] excludingList)
	{
		return FindSurvivorWithComparator(excludingList, HighestInfluenceComparator);
	}


	private PlayerPiece FindSurvivorWithComparator(PlayerPiece[] excludingList, System.Func<PlayerPiece, PlayerPiece, bool> comparator)
	{
		PlayerPiece returnSurvivor = (PlayerPiece)Positions [0].GetOccupant();

		// For each player in the player positions...
		for (int i = 0; i < Positions.Count; i++)
		{
			// ...Get the current player...
			var currentPlayer = (PlayerPiece)Positions [i].GetOccupant();

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

			if (comparator(currentPlayer, returnSurvivor))
				returnSurvivor = currentPlayer;
		}

		return returnSurvivor;
	}

	private bool LowestInfluenceComparator(PlayerPiece newSurvivor, PlayerPiece baseSurvivor)
	{
		return newSurvivor.Influence < baseSurvivor.Influence;
	}

	private bool HighestInfluenceComparator(PlayerPiece newSurvivor, PlayerPiece baseSurvivor)
	{
		return newSurvivor.Influence > baseSurvivor.Influence;
	}
}
