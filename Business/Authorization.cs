using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class Authorization
    {
        public static bool SaveToken(RefreshToken token)
        {
            return DataAccessor.MongoDBHelper.InserOne("hotel", "Authorization", token);
        }

        public static RefreshToken GetAuthorization(string Id)
        {
            Dictionary<string, object> filter = new Dictionary<string, object>();
            filter.Add("Id", Id);
            return DataAccessor.MongoHelperExtensions.FindData<RefreshToken>("hotel", "Authorization", 1, 1, filter).FirstOrDefault();
        }

        public static bool DeleteOne(string Id)
        {
            Dictionary<string, object> filter = new Dictionary<string, object>();
            filter.Add("Id", Id);
            return DataAccessor.MongoHelperExtensions.DeleteOne<RefreshToken>("hotel", "Authorization", filter);
        }
    }
}
