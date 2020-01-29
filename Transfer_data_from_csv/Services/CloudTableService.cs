using System;

using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;

namespace Transfer_data_from_csv.Services
{
   public class CloudTableService
    {
        private static CloudTableClient CreateCloudTableClient(string accountName, string accountKey)
        {
            StorageCredentials creds = new StorageCredentials(accountName, accountKey);
            CloudStorageAccount account = new CloudStorageAccount(creds, useHttps: true);

            return account.CreateCloudTableClient();
        }

        public static CloudTable GetAuthTable(string accountName, string accountKey)
        {
            CloudTableClient client = CreateCloudTableClient(accountName, accountKey);

            return client?.GetTableReference("PreliminaryData");
        } 
    }
        
}
