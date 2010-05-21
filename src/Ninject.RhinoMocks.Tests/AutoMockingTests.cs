using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Rhino.Mocks;

namespace Ninject.RhinoMocks.Tests
{
	[TestFixture]
	public class AutoMockingTests
	{
		private MockingKernel kernel;

		[SetUp]
		public void Setup()
		{
			kernel = new MockingKernel();
		}

		[Test]
		public void automocking_should_provide_the_required_mock_instance()
		{
			var SUT = kernel.Get<TestClass>();
			SUT.TestMethod();

			var something = kernel.Get<ISomething>();
			something.AssertWasCalled(s => s.DoSomething());
		}

		[TearDown]
		public void Teardown()
		{
			kernel.Reset();
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
