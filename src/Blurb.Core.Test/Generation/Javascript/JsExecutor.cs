using JavaScriptEngineSwitcher.Core;
using JavaScriptEngineSwitcher.Msie;
using React;

namespace Blurb.Core.Test.Generation.Javascript
{
	public class JsExecutor
	{
		readonly string javascript;
		readonly IReactEnvironment reactEnvironment;

		public JsExecutor(string javascript)
		{
			this.javascript = javascript;
			Initializer.Initialize(r => r.AsSingleton());

			var container = AssemblyRegistration.Container;
			container.Register<ICache, NullCache>();
			container.Register<IFileSystem, SimpleFileSystem>();
			ReactSiteConfiguration.Configuration.SetReuseJavaScriptEngines(true);

			JsEngineSwitcher.Current.DefaultEngineName = MsieJsEngine.EngineName;
			JsEngineSwitcher.Current.EngineFactories.AddMsie();

			this.reactEnvironment = ReactEnvironment.Current;

			this.reactEnvironment.Configuration.BabelConfig.Presets.Remove("react");
		}

		public string GetTerm(string key, params object[] args)
		{
			if (args != null)
			{
				
				var functionWithArgs = this.javascript + "; testTerms." + key + "('" + string.Join(',', args) + "');";
				return this.reactEnvironment.Execute<string>(functionWithArgs);
			} 

			var function = this.javascript + "; testTerms." + key + ";";
			return this.reactEnvironment.Execute<string>(function);
		}
	}
}