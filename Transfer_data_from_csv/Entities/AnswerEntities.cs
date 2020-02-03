using System;
using Microsoft.WindowsAzure.Storage.Table;

namespace Transfer_data_from_csv.Entities
{  
      public class AnswerEntities : TableEntity
      {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Answer1 { get; set; }
        public string Answer2 { get; set; }
        public string Answer3 { get; set; }
        public string Answer4 { get; set; }

        public bool IsProcessed { get; set; }
      }
}
