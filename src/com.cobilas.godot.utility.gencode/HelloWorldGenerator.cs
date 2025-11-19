#if false
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Diagnostics;
using System.Text;
using System.IO;


namespace com.cobilas.godot.utility.gencode;
[Generator]
public class HelloWorldGenerator : IIncrementalGenerator
{
	public void Initialize(IncrementalGeneratorInitializationContext context) {
		//context.RegisterSourceOutput(
		//			context.CompilationProvider,
		//			(spc, compilation) => {
		//				spc.AddSource("Hello.g.cs", """
  //                  // Auto-generated
  //                  namespace Generated
  //                  {
  //                      public static class Hello
  //                      {
  //                          public static string Message => "Hello from Incremental Generator!";
  //                      }
  //                  }
  //              """);
  //      });
	}
}
#endif