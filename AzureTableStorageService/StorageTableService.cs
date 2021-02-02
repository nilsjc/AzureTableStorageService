using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AzureTableStorageService
{
    public class StorageTableService
    {
        public async Task<ReturnObject> InsertCase(SingleNote singleCase)
        {
            var noteEntity = singleCase.ToCaseEntity();
            var insertOperation = TableOperation.InsertOrMerge(noteEntity);
            try
            {
                await CreateCloudTable(GlobalStrings.NotesTableName)
                        .ExecuteAsync(insertOperation);
                return new ReturnObject(true, noteEntity.Id);
            }
            catch (Exception e)
            {
                return new ReturnObject(false, e.Message);
            }
        }
        
        public async Task<ReturnObject> GetCase(string id, string partitionKey)
        {
            TableOperation retrieveOperation = TableOperation.Retrieve<NoteEntity>(partitionKey, id);

            TableResult retrievedResult = await CreateCloudTable(GlobalStrings.NotesTableName)
                                                .ExecuteAsync(retrieveOperation);

            try
            {
                if (retrievedResult.Result != null)
                    return new ReturnObject(true, "", retrievedResult.Result);
                else
                    return null;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        
        public async Task<ReturnObject> GetCases(string id, int num)
        {
            var records = new List<SingleNote>();
            var query = new TableQuery<NoteEntity>()
                .Where(
                TableQuery.CombineFilters(
                    TableQuery.GenerateFilterCondition("PartitionKey",
                                            QueryComparisons.Equal, id),

                    TableOperators.And,
                    TableQuery.GenerateFilterConditionForDate("Timestamp",
                                            QueryComparisons.LessThanOrEqual, DateTime.Now)

                    )
                ).Take(num);

            TableContinuationToken token = null;
            do
            {
                var resultSegment = await CreateCloudTable(GlobalStrings.NotesTableName)
                    .ExecuteQuerySegmentedAsync(query, token);

                token = resultSegment.ContinuationToken;
                resultSegment.Results.ForEach(x => records.Add(x.ToSingleCase()));
            } while (token != null && records.Count < query.TakeCount);

            //records.Sort((x, y) => y.DateTime.CompareTo(x.DateTime));
            return new ReturnObject(true, "cases returned")
            {
                Result = records
            };
        }
        
        public async Task<ReturnObject> DeleteCase(string id, string user)
        {
            var returnObject = await GetCase(id, user);
            try
            {
                var retrieveOperation = TableOperation.Delete((NoteEntity)returnObject.Result);
                var retrievedResult = await CreateCloudTable(GlobalStrings.NotesTableName)
                                            .ExecuteAsync(retrieveOperation);

                return new ReturnObject(true, "case deleted");
            }
            catch
            {
                return new ReturnObject(false, "case not deleted");
            }
        }
        

        private CloudTable CreateCloudTable(string tableName)
        {
            CloudStorageAccount storageAccount = new CloudStorageAccount
                    (new StorageCredentials(GlobalStrings.AccountName, GlobalStrings.AccountKey), true);
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            return tableClient.GetTableReference(tableName);
        }
    }
}
