using System;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;
namespace Transfer_Data_To_Cognitive_Service.Services
{
    public class CloudTableService
    {
        private CloudTableClient CreateCloudTableClient(string accountName, string accountKey)
        {
            StorageCredentials creds = new StorageCredentials(accountName, accountKey);
            CloudStorageAccount account = new CloudStorageAccount(creds, useHttps: true);
            return account.CreateCloudTableClient();
        }
        public CloudTable GetAuthTable(string accountName, string accountKey)
        {
            CloudTableClient client = CreateCloudTableClient(accountName, accountKey);
            return client.GetTableReference("PreliminaryData");
            
        }
    }

}