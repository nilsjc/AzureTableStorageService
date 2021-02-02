using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace AzureTableStorageService
{
    public class NoteEntity : TableEntity
    {
        public NoteEntity()
        {

        }
        public NoteEntity(string partitionKey, string id)
        {
            PartitionKey = partitionKey;
            RowKey = Id = id;
        }
        public string Id { get; set; }
        public string Note { get; set; }
    }
}
