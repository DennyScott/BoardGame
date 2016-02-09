using System.Collections.Generic;

public class PositionSet<T> where T : LocationPiece
{
	protected List<Position> Positions;

	public int PositionAmount = 3;

	public int Count { get; private set; }

	public void SetUp(int positionAmount)
	{
		Positions = new List<Position> (PositionAmount);
	}

	public bool Queue(T newPiece)
	{
		if (Count == PositionAmount)
			return false;
		
		// For each position in the PlayerPositions Array...
		for (int i = 0; i < Positions.Count; i++)
		{
			// ...If adding the character returns true, meaning it was added...
			if (Positions [i].AddCharacter(newPiece))
			{
				Count++;
				// ...return true.
				return true;
			}
		}

		return false;
	}

	public T Pop()
	{
		if (Count == 0)
			return null;
		
		Count--;
		var returnVal = Positions [Count].RemoveCharacter();
		return (T)returnVal;
	}

	public T Remove(T pieceToRemove)
	{
		// For each piece in the passed piece collection...
		for (int i = 0; i < Positions.Count; i++)
		{
			var removedPiece = Positions [i].RemoveCharacter(pieceToRemove);
			// ...If the piece removal resulted in a non null result, meaning something was removed...
			if (removedPiece != null)
			{
				Count--;

				// For each position, other then the final (which will be empty after this)...
				for (int x = i; x < Positions.Count - 1; x++)
				{
					// ...Take the character a position ahead and move him one back.
					Positions [x].AddCharacter(Positions [x + 1].RemoveCharacter());
				}
				// ...Then return true.
				return (T)removedPiece;
			}
		}

		return null;
	}
}
