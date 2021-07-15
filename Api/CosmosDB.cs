using System;
using System.Threading.Tasks;
using System.Configuration;
using System.Collections.Generic;
using System.Net;
using Microsoft.Azure.Cosmos;
using BlazorApp.Shared;
using IpInfo;


namespace BlazorApp.Api
{
    public class CosmosDBOps
    {
        private string EndpointUrl = Environment.GetEnvironmentVariable("COSMOS_URL");
        private string PrimaryKey = Environment.GetEnvironmentVariable("COSMOS_KEY");
        private CosmosClient cosmosClient;
        private Database database;
        private Container container;
        private string databaseId = "CosmosdbGeneralPurpose";
        private string containerId = "ContainerGeneralPurpose";

        private async Task CreateDatabaseAsync()
        {
            // Create a new database
            this.database = await this.cosmosClient.CreateDatabaseIfNotExistsAsync(databaseId);
        }

        private async Task CreateContainerAsync()
        {
            // Create a new container
            this.container = await this.database.CreateContainerIfNotExistsAsync(containerId, "/id");
        }

        private async Task AddItemsToContainerAsync<T>(T geoInfo)
        {
            try
            {
                // Create an item in the container representing the Andersen family. Note we provide the value of the partition key for this item, which is "Andersen".
                ItemResponse<T> createdItem = await this.container.CreateItemAsync<T>(geoInfo);
                // Note that after creating the item, we can access the body of the item with the Resource property of the ItemResponse. We can also access the RequestCharge property to see the amount of RUs consumed on this request.
                Console.WriteLine("Created item. Operation consumed {0} RUs.\n", createdItem.RequestCharge);
            }
            catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.Conflict)
            {
                Console.WriteLine("Item in database with id: {0} already exists\n");                
            }
        }

        public async Task StoreToCosmosDB<T>(T value)
        {
            this.cosmosClient = new CosmosClient(EndpointUrl, PrimaryKey);
            await this.CreateDatabaseAsync();
            await this.CreateContainerAsync();
            await this.AddItemsToContainerAsync<T>(value);
        }
    }
}