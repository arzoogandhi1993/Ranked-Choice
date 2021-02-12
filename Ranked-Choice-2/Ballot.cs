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
		if (ballotArr.Count > 0)
		{
			ballotArr.RemoveAt(0);
		}

		int top = ballotArr.Count > 0 ? ballotArr[0] : -1;

		if (activeCandidates.Contains(top))
		{
			candidate = top;
		}

		// Return result
		return candidate;
	}
}
