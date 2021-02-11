using System;
using System.Collections.Generic;
using System.Linq;

namespace Ranked_Choice_2
{
    class Program
    {
        static void Main(string[] args)
        {
			try
			{
				Console.WriteLine("Enter Number of Candidates");
				int n = Int32.Parse(Console.ReadLine());
				Console.WriteLine("Enter number of voters");
				int m = Int32.Parse(Console.ReadLine());
				IList<IList<int>> ballots = new List<IList<int>>();

				for (int i = 0; i < m; i++)
				{
					Console.WriteLine("Enter ballots for voter number {0} seperated by ','", i+1);
					string[] input = Console.ReadLine().Replace(" ", string.Empty).Split(',');

					List<int> ballot = input.Select(int.Parse).ToList();
					ballots.Add(ballot);
				}

				List<int> candidates = new List<int>();
				for (int i = 0; i < n; i++)
				{
					candidates.Add(i + 1);
				}


				// Initialize a new contest
				Contest contest = new Contest(candidates, ballots);
				contest.RunContest();
				// Report the winner(s)
				Console.WriteLine("The winners for the contest are : ");
				foreach(int entry in contest.Winners)
				{
					Console.WriteLine(entry);
				}
				Console.WriteLine();
			}
			catch (Exception e)
			{
				Console.WriteLine(e.ToString());
				Console.Write(e.StackTrace);
			}
		}
    }
}
