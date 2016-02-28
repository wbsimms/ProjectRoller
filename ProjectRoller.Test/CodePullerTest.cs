using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ProjectRoller.Test
{
	[TestClass]
	public class CodePullerTest
	{
		[TestMethod]
		public void ConstructorTest()
		{
			CodePuller cp = new CodePuller();
			Assert.IsNotNull(cp);
		}

		[TestMethod]
		public void GetCodeTest()
		{
			CodePuller cp = new CodePuller();
			cp.GetCode("test1");
		}

	}
}
