using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System.Text;

namespace SourceGeneratorPOC
{
    [Generator]
    public class SourceGenerator : ISourceGenerator
    {
        private const string MessagesCode = @"
namespace Generated
{
    public static class Messages
    {
        public const string Hello = ""Hello from generated code!"";

        private static void UnusedMethod() { } // sonar-dotnet issue raised here
    }
}
";

        private const string WithSecurityIssue = @"
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace Generated
{
    public partial class Unsafe : Controller
    {
        public void Index(string tainted)
        {
            new Regex(tainted).ToString(); // sonar-security issue should be raised here
        }
    }
}
";

        public void Execute(GeneratorExecutionContext context)
        {
            context.AddSource("Greetings", SourceText.From(MessagesCode, Encoding.UTF8));
            context.AddSource("Unsafe", SourceText.From(WithSecurityIssue, Encoding.UTF8));
        }

        public void Initialize(GeneratorInitializationContext context) { /* Nothing to do here */ }
    }
}
