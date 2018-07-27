using System;
using NUnit.Framework;
using ListParser.Core;

namespace ListParser.Core.Tests
{
	[TestFixture]
	public class DirectionCodeTest
	{
		[Test]
		public void CompareTest()
		{
			Assert.Less(new DirectionCode(1, 2, 3), DirectionCode.Parse("04.03.93"));
		}
	}
}
