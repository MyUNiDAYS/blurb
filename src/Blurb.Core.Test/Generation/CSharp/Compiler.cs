using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Blurb.Core.Test.Generation.CSharp
{
	public class Compiler
	{
		public Assembly CompileCSharp(string csharp)
		{
			var assemblyPath = GetAssemblyPath(typeof(ValueTuple<>).Assembly);
			var directoryName = Path.GetDirectoryName(assemblyPath);
			var netstandard = Path.Combine(directoryName, "netstandard.dll");
			var sysRuntime = Path.Combine(directoryName, "System.Runtime.dll");

			var references = new Assembly[]
				{
					typeof(object).Assembly,
					typeof(Blurb.Core.Term).Assembly, 
					this.GetType().Assembly
				}
				.Select(GetAssemblyPath)
				.Union(new [] { netstandard, sysRuntime })
				.Distinct(p => Path.GetFileName(p).ToLowerInvariant())
				.Select(p => MetadataReference.CreateFromFile(p) as MetadataReference)
				.ToList();

			var cSharpCompilationOptions = new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary);
			var compilation = CSharpCompilation.Create("assembly",
				new[] {CSharpSyntaxTree.ParseText(csharp, encoding: Encoding.UTF8)},
				references,
				cSharpCompilationOptions);

			using (var assemblyStream = new MemoryStream())
			using (var symbolStream = new MemoryStream())
			{
				var result = compilation.Emit(assemblyStream, symbolStream);

				var errors = result.Diagnostics.Where(d => d.Severity == DiagnosticSeverity.Error).ToArray();
				if (errors.Any())
				{
					var message = errors.Select(e =>
					{
						var fileLinePositionSpan = e.Location.GetMappedLineSpan();
						return
							$"[{e.Id}] File: {fileLinePositionSpan.Path}, Line: {fileLinePositionSpan.StartLinePosition.Line}, Character: {fileLinePositionSpan.StartLinePosition.Character}: `{e.GetMessage()}`";
					}).Aggregate((s1, s2) => s1 + "\n" + s2);

					throw new Exception("Failed to compile CSharp: " + message);
				}

				var assembly = Assembly.Load(assemblyStream.ToArray(), symbolStream.ToArray());
//
//				var type = assembly.GetType(view.Namespace + "." + view.ClassName);
//				if (type == null)
//					throw new ViewRenderException($"Could not find type `{view.Namespace + "." + view.ClassName}` in assembly `{assembly.FullName}`");

				return assembly;
			}
		}

		static string GetAssemblyPath(Assembly assembly)
		{
			return new Uri(assembly.EscapedCodeBase).LocalPath;
		}
	}
}