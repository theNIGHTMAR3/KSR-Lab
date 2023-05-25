using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Queue;

namespace WCFServiceWebRole1
{

	public class Service1 : IService1
	{
        public void Koduj(string nazwa, string tresc)
        {
            var account = CloudStorageAccount.DevelopmentStorageAccount;

            CloudBlobClient cloudBlobClient =account.CreateCloudBlobClient();
            CloudBlobContainer container = cloudBlobClient.GetContainerReference("blobs");
            container.CreateIfNotExists();
            var blob = container.GetBlockBlobReference(nazwa);
            var bytes = new ASCIIEncoding().GetBytes(tresc);
            var memoryStream = new MemoryStream(bytes);
            blob.UploadFromStream(memoryStream);

            CloudQueueClient cloudQueueClient =account.CreateCloudQueueClient();
            CloudQueue queue = cloudQueueClient.GetQueueReference("queue");
            queue.CreateIfNotExists();
            queue.AddMessage(new CloudQueueMessage(nazwa));
        }

        public string Pobierz(string name)
        {
            var account = CloudStorageAccount.DevelopmentStorageAccount;

            CloudBlobClient cloudBlobClient = account.CreateCloudBlobClient();
            CloudBlobContainer container = cloudBlobClient.GetContainerReference("encoded");
            container.CreateIfNotExists();

            var blob = container.GetBlockBlobReference(name);
            var memoryStream = new MemoryStream();
            blob.DownloadToStream(memoryStream);
            string content = Encoding.UTF8.GetString(memoryStream.ToArray());
            return content;
        }
    }
}
