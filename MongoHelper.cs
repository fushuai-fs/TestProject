using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessor
{
    /// <summary>
    ///  more infomation please access https://docs.mongodb.com/getting-started/csharp/
    /// </summary>
    public static class MongoHelper
    {
        /// 设置集群ip and port
        public static List<MongoServerAddress> server = new List<MongoServerAddress>() { new MongoServerAddress("127.0.0.1", 27017) };
        private static MongoClientSettings setting = new MongoClientSettings();
        // private static MongoCredential credential = MongoCredential.CreateCredential("admin", "admin", "abc123");
        private static List<MongoCredential> credential = new List<MongoCredential>() { MongoCredential.CreateCredential("admin", "admin", "abc123") };

        private static MongoClient Cliect()
        {
            MongoClient cliect = new MongoClient("mongodb://admin:abc123@127.0.0.1:27017/admin/?readPreference=PrimaryPreferred");

            return cliect;
        }
        private static void ConfigureSettings(MongoClientSettings setting)
        {
            setting.Servers = server;
            setting.Credentials = credential;
            setting.ConnectTimeout = TimeSpan.FromSeconds(20);
            setting.ConnectionMode = ConnectionMode.Direct;
            setting.GuidRepresentation = GuidRepresentation.CSharpLegacy;
            setting.IPv6 = false;
            setting.MaxConnectionIdleTime = TimeSpan.FromMinutes(10);
            setting.MaxConnectionLifeTime = TimeSpan.FromMinutes(30);
            setting.MaxConnectionPoolSize = 100;
            setting.SocketTimeout = TimeSpan.FromSeconds(30);
            setting.WaitQueueSize = 500;
            setting.WaitQueueTimeout = TimeSpan.FromMilliseconds(120000);
            setting.ReadPreference = ReadPreference.PrimaryPreferred;
        }

        /// <summary>
        /// 条件查询
        /// </summary>
        /// <typeparam name="TClass">T</typeparam>
        /// <param name="dbName">数据库</param>
        /// <param name="collectionName">集合</param>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <returns>List<TClass></returns>
        public static List<TClass> FindData<TClass>(string dbName, string collectionName, FilterDefinition<TClass> filter, SortDefinition<TClass> sort = null)
        {
            try
            {
                ConfigureSettings(setting);
                var client = new MongoClient(setting);

                var database = client.GetDatabase(dbName);
                var collection = database.GetCollection<TClass>(collectionName);

                var result = collection.Find(filter).Sort(sort);
                var test = result.ToList();
                return test;

            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <typeparam name="TClass"></typeparam>
        /// <param name="dbName"></param>
        /// <param name="collectionName"></param>
        /// <param name="tClass"></param>
        /// <returns></returns>
        public static bool InserOne<TClass>(string dbName, string collectionName, TClass tClass)
        {
            try
            {
                //  ConfigureSettings(setting);
                //var client = new MongoClient(setting);
                MongoClient client = Cliect();
                var database = client.GetDatabase(dbName);
                var collection = database.GetCollection<TClass>(collectionName);

                collection.InsertOne(tClass);
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <typeparam name="TClass"></typeparam>
        /// <param name="dbName"></param>
        /// <param name="collectionName"></param>
        /// <param name="tClass"></param>
        /// <returns></returns>
        public static bool InsertMany<TClass>(string dbName, string collectionName, IEnumerable<TClass> tClass)
        {
            try
            {
                ConfigureSettings(setting);
                var client = new MongoClient(setting);

                var database = client.GetDatabase(dbName);
                var collection = database.GetCollection<TClass>(collectionName);

                collection.InsertMany(tClass);
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <typeparam name="TClass"></typeparam>
        /// <param name="dbName"></param>
        /// <param name="collectionName"></param>
        /// <param name="filter"></param>
        /// <param name="update"></param>
        /// <returns></returns>
        public static bool UpdateOne<TClass>(string dbName, string collectionName, FilterDefinition<TClass> filter, UpdateDefinition<TClass> update)
        {
            try
            {
                ConfigureSettings(setting);
                var client = new MongoClient(setting);

                var database = client.GetDatabase(dbName);
                var collection = database.GetCollection<TClass>(collectionName);

                collection.UpdateOne(filter, update);
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <typeparam name="TClass"></typeparam>
        /// <param name="dbName"></param>
        /// <param name="collectionName"></param>
        /// <param name="filter"></param>
        /// <param name="update"></param>
        /// <returns></returns>
        public static bool UpdateMany<TClass>(string dbName, string collectionName, FilterDefinition<TClass> filter, UpdateDefinition<TClass> update)
        {
            try
            {
                ConfigureSettings(setting);
                var client = new MongoClient(setting);

                var database = client.GetDatabase(dbName);
                var collection = database.GetCollection<TClass>(collectionName);

                collection.UpdateMany(filter, update);
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <typeparam name="TClass"></typeparam>
        /// <param name="dbName"></param>
        /// <param name="collectionName"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static bool DeleteOne<TClass>(string dbName, string collectionName, FilterDefinition<TClass> filter)
        {
            try
            {
                ConfigureSettings(setting);
                var client = new MongoClient(setting);

                var database = client.GetDatabase(dbName);
                var collection = database.GetCollection<TClass>(collectionName);

                collection.DeleteOne(filter);
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <typeparam name="TClass"></typeparam>
        /// <param name="dbName"></param>
        /// <param name="collectionName"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static bool DeleteMany<TClass>(string dbName, string collectionName, FilterDefinition<TClass> filter)
        {
            try
            {
                ConfigureSettings(setting);
                var client = new MongoClient(setting);

                var database = client.GetDatabase(dbName);
                var collection = database.GetCollection<TClass>(collectionName);

                collection.DeleteMany(filter);
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
