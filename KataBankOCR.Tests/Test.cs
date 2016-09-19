using System;
using System.IO;
using NUnit.Framework;

namespace KataBankOCR.Tests
{
	[TestFixture]
	public class KataBankIntegrationTests
	{
		[Test]
		[TestCase(new string[] { }, null)]
		[TestCase(new string[] {
			"Not a full character set"
		}, "Error: Input must be of valid set of characters")]
		[TestCase(new string[] {
			" _  _  _  _  _  _  _  _  _ ",
			"| || || || || || || || || |",
			"|_||_||_||_||_||_||_||_||_|",
			"                           "
		}, "000000000")]
		[TestCase(new string[] {
			"                           ",
			"  |  |  |  |  |  |  |  |  |",
			"  |  |  |  |  |  |  |  |  |",
			"                           "
		}, "111111111 ERR")]
		[TestCase(new string[] {
			" _  _  _  _  _  _  _  _  _ ",
			" _| _| _| _| _| _| _| _| _|",
			"|_ |_ |_ |_ |_ |_ |_ |_ |_ ",
			"                           "
		}, "222222222 ERR")]
		[TestCase(new string[] {
			" _  _  _  _  _  _  _  _  _ ",
			" _| _| _| _| _| _| _| _| _|",
			" _| _| _| _| _| _| _| _| _|",
			"                           "
		}, "333333333 ERR")]
		[TestCase(new string[] {
			"                           ",
			"|_||_||_||_||_||_||_||_||_|",
			"  |  |  |  |  |  |  |  |  |",
			"                           "
		}, "444444444 ERR")]
		[TestCase(new string[] {
			" _  _  _  _  _  _  _  _  _ ",
			"|_ |_ |_ |_ |_ |_ |_ |_ |_ ",
			" _| _| _| _| _| _| _| _| _|",
			"                           "
		}, "555555555 ERR")]
		[TestCase(new string[] {
			" _  _  _  _  _  _  _  _  _ ",
			"|_ |_ |_ |_ |_ |_ |_ |_ |_ ",
			"|_||_||_||_||_||_||_||_||_|",
			"                           "
		}, "666666666 ERR")]
		[TestCase(new string[] {
			" _  _  _  _  _  _  _  _  _ ",
			"  |  |  |  |  |  |  |  |  |",
			"  |  |  |  |  |  |  |  |  |",
			"                           "
		}, "777777777 ERR")]
		[TestCase(new string[] {
			" _  _  _  _  _  _  _  _  _ ",
			"|_||_||_||_||_||_||_||_||_|",
			"|_||_||_||_||_||_||_||_||_|",
			"                           "
		}, "888888888 ERR")]
		[TestCase(new string[] {
			" _  _  _  _  _  _  _  _  _ ",
			"|_||_||_||_||_||_||_||_||_|",
			" _| _| _| _| _| _| _| _| _|",
			"                           "
		}, "999999999 ERR")]
		[TestCase(new string[] {
			" _  _  _  _  _  _  _  _    ",
			"| || || || || || || ||_   |",
			"|_||_||_||_||_||_||_| _|  |",
			"                           "
		}, "000000051")]
		[TestCase(new string[] {
			"    _  _  _  _  _  _     _ ",
			"|_||_|| || ||_   |  |  | _ ",
			"  | _||_||_||_|  |  |  | _|",
			"                           "
		}, "49006771? ILL")]
		[TestCase(new string[] {
			"    _  _     _  _  _  _  _ ",
			"  | _| _||_| _ |_   ||_||_|",
			"  ||_  _|  | _||_|  ||_| _ ",
			"                           "
		}, "1234?678? ILL")]
		public void TestInputs(string[] input, string output)
		{
			string testFile = Path.GetTempFileName();
			using (StreamWriter outputWriter = new StreamWriter(testFile))
			{
				foreach (var line in input)
					outputWriter.WriteLine(line);
			}

			using (StringWriter stringWriter = new StringWriter())
			{
				Console.SetOut(stringWriter);

				KataBank.Main(new string[] {
					testFile,
				});

				if (output != null && output.Length != 0)
					Assert.AreEqual(string.Format("{0}{1}", output, Environment.NewLine), stringWriter.ToString());
				else
					Assert.AreEqual(string.Empty, stringWriter.ToString());
			}

			File.Delete(testFile);
		}
	}
}
