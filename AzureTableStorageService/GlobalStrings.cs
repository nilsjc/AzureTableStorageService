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
        public static string PartitionKey { get; } = "my_part_key";
        public static string CasesTableName { get; } = "notes";
        public static string AccountName { get; } = "enfejkaddatabas";
        public static string AccountKey { get; } = "Pifw9FxP7GaqX7fj6E8jmXEK1HH9EUx7SlkHYwlqEkWa0NH0aCljbPA3ZxLoYKNyYkbdmmZ/TpSxXVqtECM/VA==";
    }
}
