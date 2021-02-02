using System;
using System.Collections.Generic;
using System.Text;

namespace AzureTableStorageService
{
    public static class Helpers
    {
        public static SingleNote ToSingleCase(this NoteEntity caseEntity)
        {
            return new SingleNote
            {
                Id = caseEntity.Id,
                Note = caseEntity.Note
            };
        }
        public static NoteEntity ToCaseEntity(this SingleNote singleCase)
        {
            return new NoteEntity(partitionKey: GlobalStrings.PartitionKey, id: singleCase.Id)
            {
                Note = singleCase.Note
            };
        }
    }
}
