using System.Collections.Generic;

/// <summary>
/// An object holding the ranked choices submitted by a user.
/// </summary>
public class Ballot
{
	/// <summary>
	/// An internal collection that holds the candidate in ranked order.
	/// </summary>
	private readonly List<int> ballotArr;

	
	public Ballot(IList<int> rankedChoices)
	{
		ballotArr = new List<int>(rankedChoices);
	}

	/// <summary>
	/// Get the name of the active candidate for which this Ballot is voting. </summary>
	/// <param name="activeCandidates"> All of the currently active candidates. </param>
	/// <returns> The name of the highest-ranked, non-eliminated candidate on this Ballot. </returns>
	public int getCandidateForBallot(ICollection<int> activeCandidates)
	{
		int candidate = -1;

		List<int> activeCandidatesOnThisBallot = new List<int>(ballotArr);
		activeCandidatesOnThisBallot.RetainAll(activeCandidates); // should preserve order
																  // If not all candidates on this Ballot have been eliminated
		if (activeCandidatesOnThisBallot.Count > 0)
		{
			// Return the highest-ranked candidate
			candidate = activeCandidatesOnThisBallot[0];
		}
		// Return result
		return candidate;
	}
}
