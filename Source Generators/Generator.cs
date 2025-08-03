using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniECS.SourceGenerators
{
    [Generator]
    public class PrototypeSourceGenerator : ISourceGenerator
    {
        public void Initialize(GeneratorInitializationContext context) { }

        public void Execute(GeneratorExecutionContext context)
        {
            var structsToGenerate = new List<(string structName, string targetNamespace)>();

            foreach (var syntaxTree in context.Compilation.SyntaxTrees)
            {
                var semanticModel = context.Compilation.GetSemanticModel(syntaxTree);
                var root = syntaxTree.GetRoot();

                var structDeclarations = root.DescendantNodes()
                    .OfType<StructDeclarationSyntax>();

                foreach (var structDecl in structDeclarations)
                {
                    var symbol = semanticModel.GetDeclaredSymbol(structDecl);
                    if (symbol == null) continue;

                    var attributeData = symbol.GetAttributes()
                        .FirstOrDefault(attr => attr.AttributeClass?.Name == "GeneratePrototypeAttribute");

                    if (attributeData != null)
                    {
                        string structName = symbol.Name;
                        string targetNamespace = "PrototypeGenerated"; // default fallback

                        if (attributeData.ConstructorArguments.Length == 1)
                        {
                            var nsArg = attributeData.ConstructorArguments[0];
                            if (nsArg.Value is string nsValue)
                            {
                                targetNamespace = nsValue;
                            }
                        }

                        structsToGenerate.Add((structName, targetNamespace));
                    }
                }
            }

            foreach (var (structName, targetNamespace) in structsToGenerate)
            {
                var source = $@"
using MiniECS;
namespace {targetNamespace}
{{    
    public sealed partial class {structName}Prototype : ComponentPrototype<{structName}>
    {{
        // auto generated code for {structName}Prototype
    }}
}}";
                context.AddSource($"{structName}Prototype.g.cs", SourceText.From(source, Encoding.UTF8));
            }
        }
    }
}
