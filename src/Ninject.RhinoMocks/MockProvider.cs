using System;
using System.Collections.Generic;
using Rhino.Mocks;
using Ninject.Activation;
using Ninject.Injection;

namespace Ninject.Moq
{
	/// <summary>
	/// Creates mocked instances via Moq.
	/// </summary>
	public class MockProvider : IProvider
	{
		private static readonly Dictionary<Type, ConstructorInjector> _injectors = new Dictionary<Type, ConstructorInjector>();

		/// <summary>
		/// Gets the type (or prototype) of instances the provider creates.
		/// </summary>
		public Type Type { get; private set; }

		/// <summary>
		/// Gets the injector factory component.
		/// </summary>
		public IInjectorFactory InjectorFactory { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="MockProvider"/> class.
		/// </summary>
		/// <param name="injectorFactory">The injector factory component.</param>
		public MockProvider(IInjectorFactory injectorFactory)
		{
			InjectorFactory = injectorFactory;
		}

		/// <summary>
		/// Creates an instance within the specified context.
		/// </summary>
		/// <param name="context">The context.</param>
		/// <returns>The created instance.</returns>
		public object Create(IContext context)
		{
			var mocks = new MockRepository();
			return mocks.StrictMock(Type, Type.EmptyTypes);
		}

		/// <summary>
		/// Gets a callback that creates an instance of the <see cref="MockProvider"/>.
		/// </summary>
		/// <returns>The created callback.</returns>
		public static Func<IContext, IProvider> GetCreationCallback()
		{
			return ctx => new MockProvider(ctx.Kernel.Components.Get<IInjectorFactory>());
		}
	}
}
