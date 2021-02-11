using System.Collections.Generic;

/// <summary>
/// A class representing the collection (pile) of Ballots cast for a candidate in
/// the contest.
/// </summary>
public class Pile
{
	/// <summary>
	/// The name of the candidate corresponding to this Pile of Ballots.
	/// </summary>
	private readonly int candidateNumber;

	/// <summary>
	/// The Ballots in this Pile.
	/// </summary>
	private readonly HashSet<Ballot> ballots = new HashSet<Ballot>();

	public HashSet<Ballot> Ballots
	{
		get
		{
			return this.ballots;
		}
	}

	/// <summary>
	/// Creates a new Pile of Ballots with the given name of the candidate to which this Pile
	/// will correspond.
	/// </summary>
	internal Pile(int candidateNumber)
	{
		this.candidateNumber = candidateNumber;
	}

	/// <summary>
	/// Get the candidate corresponding to this Pile of Ballots. </summary>
	/// <returns> The name of the Candidate. </returns>
	public virtual int CandidateNumber
	{
		get
		{
			return candidateNumber;
		}
	}

	/// <summary>
	/// Get the number of Ballots in this Pile. </summary>
	/// <returns> The total number of Ballots in this Pile. </returns>
	public int TotalBallots
	{
		get
		{
			return ballots.Count;
		}
	}

	public void AddBallot(Ballot ballot)
	{
		this.ballots.Add(ballot);
	}
}
