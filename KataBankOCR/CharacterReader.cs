using System;
using System.Collections.Generic;

namespace KataBankOCR
{
	static class CharacterReader
	{
		public static int? TryParse(IEnumerable<string> input)
		{
			string characterKey = string.Join("", input);

			// If the given character key does not occur in the mapping, it is invalid.
			// In this case we return null to indicate an illegible character;
			if (!s_characterMap.ContainsKey(characterKey))
				return null;

			return s_characterMap[characterKey];
		}

		// CHARACTER MAP
		// This mapping takes the character forms provided by the OCR machine,
		// written as joined strings for legibility, and matches them to the
		// appropriate int32 values.
		static Dictionary<string, int> s_characterMap = new Dictionary<string, int>
		{
			{
				" _ " +
				"| |" +
				"|_|" +
				"   ",
				0
			},
			{
				"   " +
				"  |" +
				"  |" +
				"   ",
				1
			},
			{
				" _ " +
				" _|" +
				"|_ " +
				"   ",
				2
			},
			{
				" _ " +
				" _|" +
				" _|" +
				"   ",
				3
			},
			{
				"   " +
				"|_|" +
				"  |" +
				"   ",
				4
			},
			{
				" _ " +
				"|_ " +
				" _|" +
				"   ",
				5
			},
			{
				" _ " +
				"|_ " +
				"|_|" +
				"   ",
				6
			},
			{
				" _ " +
				"  |" +
				"  |" +
				"   ",
				7
			},
			{
				" _ " +
				"|_|" +
				"|_|" +
				"   ",
				8
			},
			{
				" _ " +
				"|_|" +
				" _|" +
				"   ",
				9
			}
		};
	}
}
