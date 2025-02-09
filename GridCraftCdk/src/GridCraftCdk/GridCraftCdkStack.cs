using System;
using System.Linq;
using Amazon.CDK;
using Constructs;

namespace GridCraftCdk
{
    public abstract class GridCraftCdkStack : Stack
    {
        protected readonly string EnvironmentName;

        internal GridCraftCdkStack(Construct scope, string id)
            : base(scope, $"GridCraft{id}", new StackProps
            {
                Env = new Amazon.CDK.Environment
                {
                    Account = "141714874243",
                    Region = "ap-southeast-2",
                }
            })
        {
            EnvironmentName = scope.Node.TryGetContext("environment") as string;
            if (!Environments.AvailableEnvironments.Contains(EnvironmentName))
            {
                throw new InvalidOperationException($"Invalid environment: {EnvironmentName}");
            }
        }
    }
}
