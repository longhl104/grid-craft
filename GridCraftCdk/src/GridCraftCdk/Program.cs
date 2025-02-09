using Amazon.CDK;

namespace GridCraftCdk
{
    sealed class Program
    {
        public static void Main(string[] args)
        {
            var app = new App();
            new TableGenerationStack(app, "TableGenerationStack");

            app.Synth();
        }
    }
}
