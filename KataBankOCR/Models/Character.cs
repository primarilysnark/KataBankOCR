using System.Collections.Generic;

namespace KataBankOCR
{
	public sealed class Character
	{
		public Character(List<string> lines)
		{
			Lines = lines;
		}

		public List<string> Lines { get; }
	}
}
