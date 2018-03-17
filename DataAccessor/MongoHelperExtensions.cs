using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessor
{
    public class MongoHelperExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TClass"></typeparam>
        /// <param name="dbName"></param>
        /// <param name="collectionName"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public static List<TClass> FindData<TClass>(string dbName, string collectionName, int page, int pageSize, Dictionary<string,object> filter, Dictionary<string, int> sort = null)
        {
            FilterDefinition<TClass> filters = new BsonDocument();
            if (filter != null) 
            {
                foreach(var item in filter)
                filters = Builders<TClass>.Filter.Eq(item.Key, item.Value);
            }

            List<SortDefinition<TClass>> sortDefList = new List<SortDefinition<TClass>>();
            if (sort != null)
            {
                foreach (var item in sort) { if (item.Value == -1) { sortDefList.Add(Builders<TClass>.Sort.Descending(item.Key)); } else { sortDefList.Add(Builders<TClass>.Sort.Ascending(item.Key)); } }
            }
            SortDefinition<TClass> sorts = Builders<TClass>.Sort.Combine(sortDefList);

            return DataAccessor.MongoDBHelper.FindData<TClass>(dbName, collectionName, page, pageSize, filters, sorts);
        }

        public static bool DeleteOne<TClass>(string dbName, string collectionName, Dictionary<string,object> filter)
        {
            if (filter != null)
            {
                FilterDefinition<TClass> filters = null;
                foreach (var item in filter)
                    filters = Builders<TClass>.Filter.Eq(item.Key, item.Value);
                return DataAccessor.MongoDBHelper.DeleteOne<TClass>(dbName, collectionName, filters);
            }
            return false;
        }
    }
}
