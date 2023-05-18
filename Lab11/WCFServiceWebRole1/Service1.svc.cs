using System;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System.Linq;
using Microsoft.WindowsAzure.Storage.Blob;
using System.IO;
using System.Text;

namespace WCFServiceWebRole1
{
	public class Service1 : IService1
	{
        // zad 1
        public bool Create(string login, string password)
        {
            CloudTable table = GetTableFromAzure("users");

            var checkIfUserExists = TableOperation.Retrieve<User>(login, password);
            var validationResult = table.Execute(checkIfUserExists);

            if (validationResult.Result != null)
            {
                return false;
            }

            var user = new User(login, password)
            {
                Login = login,
                Password = password,
                SessionId = Guid.Empty
            };

            var operation = TableOperation.Insert(user);
            var result = table.Execute(operation);

            if (result.Result == null)
            {
                return false;
            }

            return true;
        }

        // zad 2
        public Guid Login(string login, string password)
        {
            CloudTable table = GetTableFromAzure("users");

            var checkIfUserExists = TableOperation.Retrieve<User>(login, password);
            var result = table.Execute(checkIfUserExists);
            var user = result.Result as User;

            if (user == null)
            {
                return Guid.Empty;
            }

            var sessionId = Guid.NewGuid();
            user.SessionId = sessionId;

            var updateOperation = TableOperation.Replace(user);
            table.Execute(updateOperation);

            return sessionId;
        }

        // zad 3
        public bool Logout(string login)
        {
            CloudTable table = GetTableFromAzure("users");

            TableQuery<User> query = new TableQuery<User>()
                .Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, login));
            var result = table.ExecuteQuery(query);
            var user = result.SingleOrDefault();

            if (user == null)
            {
                return false;
            }

            user.SessionId = Guid.Empty;

            var loguotOperation = TableOperation.Replace(user);
            table.Execute(loguotOperation);

            return true;
        }

        // zad 4
        public bool Put(string name, string value, Guid sessionId)
        {
            var table = GetTableFromAzure("users");
            var blobContainer = GetBlobFromAzure("files");

            TableQuery<User> query = new TableQuery<User>()
                .Where(TableQuery.GenerateFilterConditionForGuid("SessionId", QueryComparisons.Equal, sessionId));
            var result = table.ExecuteQuery(query);
            var user = result.SingleOrDefault();

            if (user == null)
            {
                return false;
            }

            var nameOfBlob = user.Login + "_" + name;
            var blob = blobContainer.GetBlockBlobReference(nameOfBlob);

            var bytes = new ASCIIEncoding().GetBytes(value);
            var stream = new MemoryStream(bytes);
            blob.UploadFromStream(stream);

            return true;
        }

        // zad 5
        public string Get(string name, Guid sessionId)
        {
            var table = GetTableFromAzure("users");
            var blobContainer = GetBlobFromAzure("files");

            TableQuery<User> query = new TableQuery<User>()
                .Where(TableQuery.GenerateFilterConditionForGuid("SessionId", QueryComparisons.Equal, sessionId));
            var result = table.ExecuteQuery(query);
            var user = result.SingleOrDefault();

            if (user == null)
            {
                return string.Empty;
            }

            var nameOfBlob = user.Login + "_" + name;
            var blob = blobContainer.GetBlockBlobReference(nameOfBlob);

            if (blob == null)
            {
                return string.Empty;
            }

            var stream = new MemoryStream();

            try
            {
                blob.DownloadToStream(stream);
            }
            catch (Exception e)
            {
                return string.Empty;
            }
            

            string content = Encoding.UTF8.GetString(stream.ToArray());

            return content;
        }
        
        // static azure helper functions
        private static CloudTable GetTableFromAzure(string tableName)
        {
            var account = CloudStorageAccount.DevelopmentStorageAccount;
            CloudTableClient client = account.CreateCloudTableClient();
            var table = client.GetTableReference(tableName);
            table.CreateIfNotExists();
            return table;
        }

        private static CloudBlobContainer GetBlobFromAzure(string blobContainerName)
        {
            var account = CloudStorageAccount.DevelopmentStorageAccount;
            CloudBlobClient client = account.CreateCloudBlobClient();
            CloudBlobContainer container = client.GetContainerReference(blobContainerName);
            container.CreateIfNotExists();
            return container;
        }
    }
}
