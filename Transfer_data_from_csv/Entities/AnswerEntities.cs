﻿using System;
using Microsoft.WindowsAzure.Storage.Table;

namespace Transfer_data_from_csv.Entities
{  
      public class AnswerEntities : TableEntity
      {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Answer { get; set; }
        public bool IsProcessed { get; set; }
      }
}
