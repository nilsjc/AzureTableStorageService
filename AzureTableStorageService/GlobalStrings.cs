using System;
using System.Collections.Generic;
using System.Text;

namespace AzureTableStorageService
{
    /// <summary>
    /// Global strings. Use config file instead or similar.
    /// </summary>
    public static class GlobalStrings
    {
        public static string PartitionKey { get; } = "your_partitionkey";
        public static string NotesTableName { get; } = "your_tablename";
        public static string AccountName { get; } = "your_accountname";
        public static string AccountKey { get; } = "your_accountkey";
    }
}
