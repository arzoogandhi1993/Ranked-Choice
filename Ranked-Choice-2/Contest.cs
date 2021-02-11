using System.Collections.Generic;

/// <summary>
/// A class representing a ranked-choice voting contest.
/// </summary>
public class Contest
{
	/// <summary>
	/// List maintaining all the rounds and their state
	/// </summary>
	private readonly List<Round> rounds = new List<Round>();

	/// <summary>
	/// All of the Piles corresponding to each of the candidates in the Contest at any given point in time
	/// key = candidate number, value = corresponding pile
	/// </summary>
	private readonly Dictionary<int, Pile> piles;

	/// <summary>
	/// The names of the candidates that have not been eliminated yet.
	/// </summary>
	private HashSet<int> activeCandidates;

	/// <summary>
	/// The initial candidate list
	/// </summary>
	private readonly HashSet<int> allCandidates;

	public Contest(ICollection<int> candidates, IList<IList<int>> rawBallots)
	{
		this.allCandidates = new HashSet<int>(candidates);
		
		this.activeCandidates = new HashSet<int>(candidates);
		
		piles = new Dictionary<int, Pile>(candidates.Count);

		foreach (int candidate in candidates)
		{
			Pile pile = new Pile(candidate);
			piles[candidate] = pile;
		}

		// Default pile whose ballots will be distributed amongst the candidates.
		Pile initialPile = new Pile(0);

		foreach (IList<int> rawBallot in rawBallots)
		{
			// Add all the initial ballots to initial pile
			Ballot ballot = new Ballot(rawBallot);
			initialPile.AddBallot(ballot);
		}

		// Distribute the initial ballots to its own pile
		redistributeBallots(initialPile);
	}

	public void RunContest()
	{
		Round round;
		do
		{
			// Start a new Round
			round = createNewRound();
			rounds.Add(round);
			activeCandidates = round.GetRemainingActiveCandidates;
			eliminateCandidates();
		} while (activeCandidates.Count > 0);
	}

	/// <summary>
	/// Creates a new Round of the Contest with the Piles of the active candidates. </summary>
	/// <returns> A new Round with the Piles of the active candidates. </returns>
	private Round createNewRound()
	{
		HashSet<Pile> activeCandidatesPiles = new HashSet<Pile>();

		foreach (int activeCandidate in activeCandidates)
		{			
			activeCandidatesPiles.Add(piles[activeCandidate]);
		}
		
		return new Round(activeCandidatesPiles);
	}

	/// <summary>
	/// Redistribute the Ballots of any candidates that are no longer active.
	/// </summary>
	private void eliminateCandidates()
	{
		foreach (int candidate in allCandidates)
		{
			// If the candidate has been eliminated, i.e. is no longer active
			if (!activeCandidates.Contains(candidate))
			{
				// Redistribute the Ballots of its corresponding Pile
				Pile pile = piles[candidate];
				redistributeBallots(pile);
			}
		}
	}

	/// <summary>
	/// Redistributes the Ballots in the given Pile to the Piles of the active
	/// candidates. </summary>
	/// <param name="pile"> The Pile whose Ballots are to be redistributed. </param>
	private void redistributeBallots(Pile pile)
	{
		int candidate;
		Pile votePile;
		// For each Ballot in the Pile
		foreach (Ballot ballot in pile.Ballots)
		{
			// Get the candidate for which the Ballot is voting
			candidate = ballot.getCandidateForBallot(activeCandidates);
			// If this Ballot is casting a vote
			if (candidate > 0)
			{
				// Get the corresponding Pile for this candidate
				votePile = piles[candidate];
				// Add this Ballot to the Pile
				votePile.AddBallot(ballot);
			}
		}
	}

	/// <summary>
	/// Get the winners of the Contest
    /// </summary>
	/// <returns> The winner's of the Contest. Contains multiple winners in the case of a tie. </returns>
	public HashSet<int> Winners
	{
		get
		{
			// Initialize the tracker of the top candidates
			HashSet<int> topCandidates = new HashSet<int>(allCandidates);
			// Initialize the Round tracker
			int roundNum = rounds.Count - 1;
			Round currentRound;
			// While there is a tie and there are still Rounds left to check
			while ((topCandidates.Count != 1) && (roundNum >= 0))
			{
				// Get the Round currently being analyzed
				currentRound = rounds[roundNum];
				// Get the candidates among the top candidates with the most votes
				// in this round; this is the new list of top candidates
				topCandidates = currentRound.findTopCandidates(topCandidates);
				// Set next iteration to consider the next Round
				roundNum--;
			}
			// The set of top candidates has been narrowed down to the winner(s); return them
			return topCandidates;
		}
	}
}