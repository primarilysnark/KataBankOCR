using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace KataBankOCR
{
	static class AccountReader
	{
		public static ReadOnlyCollection<Account> Parse(List<string> input)
		{
			// If our input isn't divisible by our entry length, we are missing part of the input
			if (input.Count % c_entryLength != 0)
				throw new ArgumentException("Error: Input must be of valid set of characters");

			// If any of our input isn't the proper length, we are missing part of a number
			if (input.Any(line => line.Length != c_lineLength))
				throw new ArgumentException("Error: All input lines must be exactly " + c_lineLength + " characters in length");

			var accountCollection = new List<Account>();

			// Now that we have a valid input to loop on, parse each block of lines
			for (int inputIndex = 0; inputIndex < input.Count; inputIndex += c_entryLength)
			{
				// Input is given as a complete set of all lines
				// We need to break this down into blocks of four lines
				List<string> entryLines = input.GetRange(inputIndex, c_entryLength);
				List<int?> parsedNumbers = new List<int?>();

				// Parse lines into sets of nine characters
				for (int index = 0; index < c_lineLength; index += c_characterWidth)
					parsedNumbers.Add(CharacterReader.TryParse(entryLines.Select(line => line.Substring(index, c_characterWidth)).ToList()));

				// Combine characters into an account number
				accountCollection.Add(new Account(
					numbers: parsedNumbers.AsReadOnly()
				));
			}

			return accountCollection.AsReadOnly();
		}

		const int c_characterWidth = 3;
		const int c_lineLength = 27;
		const int c_entryLength = 4;
	}
}
