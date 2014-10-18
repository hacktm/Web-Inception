using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Feromon.Web.Services
{
    public class AzureStorageService
    {
        private static CloudStorageAccount storageAccount { get; set; }
        private static CloudBlobClient blobClient { get; set; }

        public AzureStorageService()
        {
            // Retrieve storage account from connection string.
            storageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["StorageConnectionString"]);
            // Create the blob client.
            blobClient = storageAccount.CreateCloudBlobClient();
        }

        public CloudBlobContainer CreateContainer(string ContainerName)
        {

            // Retrieve a reference to a container. 
            var container = blobClient.GetContainerReference(ContainerName);

            // Create the container if it doesn't already exist.
            container.CreateIfNotExists();

            container.SetPermissions(
                new BlobContainerPermissions
                {
                    PublicAccess =
                        BlobContainerPublicAccessType.Blob
                });

            return container;
        }

        public string UploadBlobToContainer(string ContainerName, string BlobName, Stream fileBase)
        {
            // Retrieve reference to a container.
            var container = blobClient.GetContainerReference(ContainerName);
            // Retrieve reference to a blob named #BlobName.
            var blockBlob = container.GetBlockBlobReference(BlobName);

            blockBlob.UploadFromStream(fileBase);

            return blockBlob.StorageUri.PrimaryUri.ToString();
        }

        public void DeleteBlob(string ContainerName, string BlobName)
        {
            // Retrieve reference to a container.
            var container = blobClient.GetContainerReference(ContainerName);

            // Retrieve reference to a blob named #BlobName.
            var blockBlob = container.GetBlockBlobReference(BlobName);

            // Delete the blob.
            blockBlob.Delete();
        }
    }
}