using System.Collections.Generic;
using System.Linq;

/// <summary>
/// A class representing the vote distributions of a round of a Contest.
/// </summary>
public class Round
{
	/// <summary>
	/// The distribution by candidate name of the votes for this Round
	/// </summary>
	private readonly Dictionary<int, int> voteDistribution;

	/// <summary>
	/// Create a new Round of the Contest. </summary>
	/// <param name="piles"> A list of the Piles of the candidates in the Contest. </param>
	public Round(ICollection<Pile> piles)
	{
		voteDistribution = new Dictionary<int, int>(piles.Count);
		// For each pile
		foreach (Pile pile in piles)
		{
			// Get the candidate number
			int candidateNumber = pile.CandidateNumber;
			// Get the number of ballots/votes in the pile
			int votes = pile.TotalBallots;
			// Store the number of votes with the name of the candidate
			voteDistribution[candidateNumber] = votes;
		}
	}

	/// <summary>
	/// Get the number of votes for each candidate this Round. </summary>
	/// <returns> A copy of the distribution of the votes over all candidates. </returns>
	public Dictionary<int, int> VoteDistribution
	{
		get
		{
			return new Dictionary<int, int>(voteDistribution);
		}
	}

	/// <summary>
	/// Get the number of votes of a particular candidate in this Round. Any
	/// candidate not in this Round will report as having zero votes. </summary>
	/// <param name="candidate"> The name of the candidate. </param>
	/// <returns> The total number of votes in this Round for the candidate. </returns>
	public virtual int getVotes(int candidate)
	{
		int votes = 0; // If the candidate cannot be found in this Round for whatever reason, they have 0 votes
					   // If the candidate is in this Round
		if (voteDistribution.ContainsKey(candidate))
		{
			// Get their total number of votes
			votes = voteDistribution[candidate];
		}
		// Return vote total
		return votes;
	}

	/// <summary>
	/// Get the remaining active candidates who were not eliminated this Round
    /// </summary>
	public HashSet<int> GetRemainingActiveCandidates
	{
		get
		{
			Dictionary<int, int>.ValueCollection voteTotals = voteDistribution.Values;
			int minVotes = voteTotals.Min();
			ISet<int> candidates = new HashSet<int>(voteDistribution.Keys);
			HashSet<int> nonEliminatedCandidates = new HashSet<int>();
			int votes;
			foreach (int candidate in candidates)
			{
				votes = voteDistribution[candidate];
				if (votes != minVotes)
				{
					nonEliminatedCandidates.Add(candidate);
				}
			}
			return nonEliminatedCandidates;
		}
	}


	/// <summary>
	/// Get the candidate(s) among the given candidates with the most votes in
	/// this Round. </summary>
	/// <param name="restriction"> A (nonempty) collection of candidates. </param>
	/// <returns> The set of top candidates among the given candidates. </returns>
	public HashSet<int> findTopCandidates(ICollection<int> restriction)
	{
		if (restriction.Count == 0)
		{
			return new HashSet<int>();
		}
		Dictionary<int, int> voteDistRestricted = new Dictionary<int, int>(voteDistribution);
		foreach (int candidate in voteDistribution.Keys)
		{
			if (!restriction.Contains(candidate))
			{
				voteDistRestricted.Remove(candidate);
			}
		}
		Dictionary<int, int>.ValueCollection voteTotals = voteDistRestricted.Values;
		int maxVotes = voteTotals.Max();
		ISet<int> candidates = new HashSet<int>(voteDistRestricted.Keys);
		HashSet<int> topCandidates = new HashSet<int>();
		int votes;
		foreach (int candidate in candidates)
		{
			votes = voteDistRestricted[candidate];
			if (votes == maxVotes)
			{
				topCandidates.Add(candidate);
			}
		}
		return topCandidates;
	}

}
