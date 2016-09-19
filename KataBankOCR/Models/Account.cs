using System.Collections.ObjectModel;
using System.Linq;

namespace KataBankOCR
{
	public class Account
	{
		public Account(ReadOnlyCollection<int?> numbers)
		{
			Numbers = numbers;
		}

		public bool IsLegible
		{
			get
			{
				return Numbers.All(number => number.HasValue);
			}
		}

		public bool IsValid
		{
			get
			{
				// If we can't determine all numbers in the account number,
				// it can't be valid
				if (!IsLegible)
					return false;

				// Account numbers are nine characters long. We can validate them by 
				// checksuming based on character position.
				//
				// account number:  3  4  5  8  8  2  8  6  5
				// position names:  d9 d8 d7 d6 d5 d4 d3 d2 d1
				//
				// checksum calculation:
				// (d1 + 2 * d2 + 3 * d3 +..+ 9 * d9) mod 11 == 0
				return Numbers.Select((number, index) => number * (Numbers.Count - index))
					.Sum() % 11 == 0;
			}
		}

		public ReadOnlyCollection<int?> Numbers { get; }
	}
}
