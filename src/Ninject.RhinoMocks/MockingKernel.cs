using System;
using Ninject.Activation;
using Ninject.Activation.Caching;
using Ninject.Planning.Bindings;

namespace Ninject.RhinoMocks
{
	/// <summary>
	/// A kernel that will create mocked instances (via Moq) for any service that is
	/// requested for which no binding is registered.
	/// </summary>
	public class MockingKernel : StandardKernel
	{
		private static readonly object StaticScope = new object();

		/// <summary>
		/// 
		/// </summary>
		public void Reset()
		{
			Components.Get<ICache>().Clear();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="service"></param>
		/// <returns></returns>
		protected override bool HandleMissingBinding(Type service)
		{
			bool selfBindable = TypeIsSelfBindable(service);

			if (selfBindable)
				Bind(service).ToSelf().InSingletonScope();
			else
			{
				var binding = new Binding(service)
					{
						ProviderCallback = MockProvider.GetCreationCallback(),
						ScopeCallback = ctx => StaticScope, 
						IsImplicit = true
					};

				AddBinding(binding);
			}
			return true;
		}

	}
}
