using System;
using Amazon.CDK;
using Amazon.CDK.AWS.S3;
using Constructs;

namespace GridCraftCdk
{
    public class TableGenerationStack : GridCraftCdkStack
    {
        internal TableGenerationStack(Construct scope, string id) : base(scope, id)
        {
            var bucketId = $"{EnvironmentName}-{StackName}-bucket".ToLowerInvariant();
            var bucket = new Bucket(this, bucketId, new BucketProps
            {
                BucketName = bucketId,
                RemovalPolicy = RemovalPolicy.DESTROY,
                AutoDeleteObjects = true,
            });
        }
    }
}