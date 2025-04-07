using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.Extensions.Configuration;
using ProductManagement.Application.Interfaces.Services;

namespace ProductManagement.Infrastructure.Services
{
    public class StorageService : IStorageService
    {
        private readonly IAmazonS3 _s3Client;
        private readonly string _bucketName;

        public StorageService(IConfiguration configuration)
        {
            var awsOptions = configuration.GetSection("AWS");

            var s3Config = new AmazonS3Config
            {
                ServiceURL = awsOptions["ServiceURL"],
                ForcePathStyle = true
            };

            _s3Client = new AmazonS3Client(
                awsOptions["AccessKey"],
                awsOptions["SecretKey"],
                s3Config
            );

            _bucketName = awsOptions["BucketName"] ?? string.Empty;
        }

        public async Task<string> UploadFileAsync(Stream fileStream, string fileName, string contentType)
        {
            var request = new PutObjectRequest
            {
                BucketName = _bucketName,
                Key = fileName,
                InputStream = fileStream,
                ContentType = contentType,
                CannedACL = S3CannedACL.PublicRead
            };

            await _s3Client.PutObjectAsync(request);
            return $"http://localstack:4566/{_bucketName}/{fileName}";
        }
    }
}
