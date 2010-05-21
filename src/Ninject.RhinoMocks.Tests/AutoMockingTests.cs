using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject.Moq;
using NUnit.Framework;

namespace Ninject.RhinoMocks.Tests
{
	[TestFixture]
	public class AutoMockingTests
	{

		[Test]
		public void automocking_should_provide_the_required_mock_instance()
		{
			MockingKernel kernel = new MockingKernel();
			kernel.Bind<ISomething>().ToMock();
			var SUT = kernel.Get<TestClass>();
			SUT.TestMethod();
		}


	}

	public interface ISomething
	{
		void DoSomething();
	}

	public class TestClass
	{
		public ISomething Something { get; set; }

		public TestClass(ISomething something)
		{
			Something = something;
		}

		public void TestMethod()
		{
			Something.DoSomething();
		}
	}
}
