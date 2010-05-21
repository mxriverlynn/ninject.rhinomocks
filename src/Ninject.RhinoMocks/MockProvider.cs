using System;
using Rhino.Mocks;
using Ninject.Activation;
using Ninject.Injection;

namespace Ninject.RhinoMocks
{
	/// <summary>
	/// Creates mocked instances via Moq.
	/// </summary>
	public class MockProvider : IProvider
	{
		/// <summary>
		/// Gets the type (or prototype) of instances the provider creates.
		/// </summary>
		public Type Type { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="MockProvider"/> class.
		/// </summary>
		///	<param name="type">the Type of object being mocked</param>
		public MockProvider(Type type)
		{
			Type = type;
		}

		/// <summary>
		/// Creates an instance within the specified context.
		/// </summary>
		/// <param name="context">The context.</param>
		/// <returns>The created instance.</returns>
		public object Create(IContext context)
		{
			return MockRepository.GenerateMock(Type, new Type[0], new object[0]);
		}

		/// <summary>
		/// Gets a callback that creates an instance of the <see cref="MockProvider"/>.
		/// </summary>
		/// <returns>The created callback.</returns>
		public static Func<IContext, IProvider> GetCreationCallback()
		{
			return ctx => new MockProvider(ctx.Request.Service);
		}
	}
}
