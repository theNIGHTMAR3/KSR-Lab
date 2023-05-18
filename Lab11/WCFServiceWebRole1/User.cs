using Microsoft.WindowsAzure.Storage.Table;
using System;

namespace WCFServiceWebRole1
{
    public class User : TableEntity
    {
        public User()
        {
        }

        public User(string partitionKey, string rowKey) : base(partitionKey, rowKey)
        {
            this.PartitionKey = partitionKey; // ustawiamy klucz partycji
            this.RowKey = rowKey; // ustawiamy klucz główny 
        }

        public string Login { get; set; }
        public string Password { get; set; }
        public Guid SessionId { get; set; }
    }
}