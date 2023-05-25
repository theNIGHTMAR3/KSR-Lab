using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Queue;
using System.IO;
using System.Text;

namespace WorkerRole1
{
    public class WorkerRole : RoleEntryPoint
    {
        private readonly CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        private readonly ManualResetEvent runCompleteEvent = new ManualResetEvent(false);

        public override void Run()
        {
            Trace.TraceInformation("---WorkerRole1 zostal uruchomiony---");
            var account = CloudStorageAccount.DevelopmentStorageAccount;
            CloudQueueClient queueCloudClient = account.CreateCloudQueueClient();
            CloudBlobClient blobCloudClient = account.CreateCloudBlobClient();
            CloudBlobClient encodedBlobCloudClient = account.CreateCloudBlobClient();
            while (true)
            {
                CloudQueue queue = queueCloudClient.GetQueueReference("queue");
                CloudBlobContainer container = blobCloudClient.GetContainerReference("blobs");
                queue.CreateIfNotExists();
                container.CreateIfNotExists();

                var nameMessage = queue.GetMessage();
                if (nameMessage == null) continue;
                queue.DeleteMessage(nameMessage);

                Trace.WriteLine($"Nazwa bloba: {nameMessage.AsString}.");

                var blob = container.GetBlockBlobReference(nameMessage.AsString);
                var memoryStream = new MemoryStream();
                blob.DownloadToStream(memoryStream);
                string message = Encoding.UTF8.GetString(memoryStream.ToArray());

                Trace.WriteLine($"Tresc wiadomosci: {message}.");
                string encoded = null;
                Random rnd = new Random();

                MessageEncoder encoder = new MessageEncoder();
                do encoded = encoder.inputEncode(message, rnd);
                while (encoded == null);

                Trace.WriteLine($"Wiadomosc w formie zaszyfrowanej przy pomocy ROT13: {encoded}.");


                CloudBlobContainer containerencoded = encodedBlobCloudClient.GetContainerReference("encoded");
                containerencoded.CreateIfNotExists();
                var blobencoded = containerencoded.GetBlockBlobReference(nameMessage.AsString);
                var bytesencoded = new ASCIIEncoding().GetBytes(encoded);
                var s = new MemoryStream(bytesencoded);

                blobencoded.UploadFromStream(s);
            }
        }

        public override bool OnStart()
        {
            ServicePointManager.DefaultConnectionLimit = 10;
            bool result = base.OnStart();
            Trace.TraceInformation("---Worker started---");
            return result;
        }

        public override void OnStop()
        {
            this.cancellationTokenSource.Cancel();
            this.runCompleteEvent.WaitOne();
            base.OnStop();
            Trace.TraceInformation("---Worker stopped---");
        }
    }

    static class Rot13
    {
        /// <summary>
        /// Performs the ROT13 character rotation.
        /// </summary>
        public static string Transform(string value)
        {
            char[] array = value.ToCharArray();
            for (int i = 0; i < array.Length; i++)
            {
                int number = (int)array[i];

                if (number >= 'a' && number <= 'z')
                {
                    if (number > 'm')
                    {
                        number -= 13;
                    }
                    else
                    {
                        number += 13;
                    }
                }
                else if (number >= 'A' && number <= 'Z')
                {
                    if (number > 'M')
                    {
                        number -= 13;
                    }
                    else
                    {
                        number += 13;
                    }
                }
                array[i] = (char)number;
            }
            return new string(array);
        }
    }


    public class MessageEncoder
    {

        public string inputEncode(string input, Random rnd)
        {

            int randomNum = rnd.Next(0, 3);
            Trace.TraceInformation("zaczynam kodowanie wiadomosci");

            if (randomNum != 0)
            {
                if (string.IsNullOrEmpty(input)) return input;

                string result = Rot13.Transform(input);
                return result;
            }

            else
            {
                Trace.TraceInformation("!!!ERROR!!!");
				//return null;

				try
				{
				    throw new Exception();
				}
				catch
				{
				    Trace.TraceInformation("zostal wyrzucony wyjatek ");
				    return null;
				
				}
			}
		}
    }
}
