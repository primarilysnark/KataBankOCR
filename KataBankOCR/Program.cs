using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace KataBankOCR
{
	public class KataBank
	{
		public static void Main(string[] args)
		{
			// This program takes the path of an OCR'd file to parse for account numbers.
			// The path is specified as an argument when calling the program.

			// If we don't have exactly one argument, we weren't passed the path properly.
			if (args.Length != 1)
			{
				Console.WriteLine("Error: File path to parse must be passed as an argument.");

				return;
			}

			// If the file does not exist at said path, we can't read it properly.
			if (!File.Exists(args[0]))
			{
				Console.WriteLine("Error: File path provided does not exist");

				return;
			}

			// Read passed file as input to parse for accounts
			List<string> input = File.ReadAllLines(args[0]).ToList();
			ReadOnlyCollection<Account> accounts;

			// In the event that we have invalid account input, this will throw.
			// TODO (Josh): Move to non-throw-based logic with better return structure.
			try
			{
				accounts = AccountReader.Parse(input);
			}
			catch (ArgumentException exception)
			{
				Console.WriteLine(exception.Message);

				return;
			}

			// Basic printing of account numbers
			// If the account number is valid, print just the number, e.g. "000000051"
			// If the account number is illegible, print the number with question marks for illegible characters
			// followed by "ILL", e.g. "49006771? ILL"
			// If the account number is invalid (per checksum rules in accounts), print the number followed
			// by "ERR", e.g. "888888888 ERR"
			foreach (Account account in accounts)
			{
				string accountNumber = string.Join("", account.Numbers.Select(number => number.HasValue ? number.ToString() : "?"));
				string status;

				if (account.IsValid)
					status = "";
				else if (account.IsLegible)
					status = " ERR";
				else
					status = " ILL";

				Console.WriteLine(accountNumber + status);
			}
		}
	}
}
