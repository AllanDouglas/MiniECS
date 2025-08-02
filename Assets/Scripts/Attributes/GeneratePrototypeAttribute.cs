namespace MiniECS
{
    [System.AttributeUsage(System.AttributeTargets.Struct)]
    public class GeneratePrototypeAttribute : System.Attribute
    {
        public string Namespace { get; private set; }

        public GeneratePrototypeAttribute(string targetNamespace = "Game")
        {
            Namespace = targetNamespace;
        }
    }
}