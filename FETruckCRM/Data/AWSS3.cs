//using Amazon;
//using Amazon.S3;
//using Amazon.S3.Model;
//using Amazon.S3.Transfer;
//using System.IO;

//namespace TruckCRM.Data
//{
//    public class S3Service
//    {
//        private readonly string _accessKey = System.Configuration.ConfigurationManager.AppSettings["AWSAccessKey"];
//        private readonly string _secretKey = System.Configuration.ConfigurationManager.AppSettings["AWSSecretKey"];
//        private readonly string _region = System.Configuration.ConfigurationManager.AppSettings["AWSRegion"];
//        private readonly string _bucketName = System.Configuration.ConfigurationManager.AppSettings["AWSBucketName"];
//        private readonly IAmazonS3 _s3Client;

//        public S3Service()
//        {
//            _s3Client = new AmazonS3Client(_accessKey, _secretKey, RegionEndpoint.GetBySystemName(_region));
//        }

//        // Synchronous method to upload file to S3
//        public void UploadFile(Stream inputStream, string key)
//        {
//            var uploadRequest = new TransferUtilityUploadRequest
//            {
//                InputStream = inputStream,
//                Key = key,
//                BucketName = _bucketName,
//                ContentType = "application/octet-stream"
//            };

//            var transferUtility = new TransferUtility(_s3Client);
//            transferUtility.Upload(uploadRequest);  // This is synchronous
//        }

//        // Synchronous method to download file from S3
//        public Stream DownloadFile(string key)
//        {
//            var request = new GetObjectRequest
//            {
//                BucketName = _bucketName,
//                Key = key
//            };
//            var response = _s3Client.GetObject(request);  // Synchronous method
//            return response.ResponseStream;
//        }

//        public void DeleteFile(string key)
//        {
//            var request = new DeleteObjectRequest
//            {
//                BucketName = _bucketName,
//                Key = key
//            };

//            _s3Client.DeleteObject(request);
//        }

//    }


//}