using UnityEngine;
using System.Collections;

public class PlayerPiece : LocationPiece
{
	public SurvivorScriptableObject survivorStats;

	/// <summary>
	/// Gets the name of the survivor.
	/// </summary>
	/// <value>The name.</value>
	public string Name { get { return survivorStats.Name; } }

	/// <summary>
	/// Gets the infulence of the survivor.
	/// </summary>
	/// <value>The infulence skill.</value>
	public int Infulence { get { return survivorStats.Influence; } }

	/// <summary>
	/// Gets the search of the survivor.
	/// </summary>
	/// <value>The search skill.</value>
	public int Search { get { return survivorStats.Search; } }

	/// <summary>
	/// Gets the attack of the survivor.
	/// </summary>
	/// <value>The attack skill.</value>
	public int Attack { get { return survivorStats.Attack; } }

	/// <summary>
	/// Gets the portrait of the survivor.
	/// </summary>
	/// <value>The portrait.</value>
	public Sprite Portrait { get { return survivorStats.Portrait; } }

	//TODO: Just like the comment in SurvivorScriptableObject, this will not be a string later
	/// <summary>
	/// Gets the skill of the survivor.
	/// </summary>
	/// <value>The skill.</value>
	public string Skill { get { return survivorStats.Skill; } }

	public override void DestroyPiece()
	{
		throw new System.NotImplementedException ();
	}
}
