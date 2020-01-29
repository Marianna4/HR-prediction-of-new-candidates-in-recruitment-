using System;
using Microsoft.WindowsAzure.Storage.Table;

namespace Transfer_Data_To_Cognitive_Service.Entities
{
    public class EntitiesFromTable : TableEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Answer { get; set; }
        public bool IsProcessed { get; set; }
    }
}