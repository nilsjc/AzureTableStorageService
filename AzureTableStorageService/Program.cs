using System;
using System.Threading.Tasks;

namespace AzureTableStorageService
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var sts = new StorageTableService();
            Console.WriteLine("select 1 for write note");
            Console.WriteLine("select 2 for read note");
            string select = Console.ReadLine();
            switch (select)
            {
                case "1":
                    Console.WriteLine("Insert id");
                    string id1 = Console.ReadLine();
                    Console.WriteLine("Insert note");
                    string note = Console.ReadLine();
                    await sts.InsertCase(new SingleNote { Id = id1, Note = note });
                    break;

                case "2":
                    Console.WriteLine("Select note ID");
                    string id2 = Console.ReadLine();
                    var result = await sts.GetCase(id2, GlobalStrings.PartitionKey);
                    // Note: Explicit conversion needed here because
                    // "ReturnObject" contains base object. Maybe good
                    // idea to change this.
                    var caseEntity = (NoteEntity)result.Result;
                    Console.WriteLine($"Id:{caseEntity.Id}  Note:{caseEntity.Note}");
                    break;
            }
        }
    }
}
