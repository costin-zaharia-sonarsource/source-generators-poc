using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System.Text;

namespace SourceGeneratorPOC
{
    [Generator]
    public class SourceGenerator : ISourceGenerator
    {
        private const string MessagesCode = @"namespace Generated
{
    public static class Messages
    {
        public const string Hello = ""Hello from generated code!"";
    }
}
";
        public void Execute(GeneratorExecutionContext context)
        {
            context.AddSource("Greetings", SourceText.From(MessagesCode, Encoding.UTF8));
        }

        public void Initialize(GeneratorInitializationContext context) { }
    }
}
