using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class httphelper
    { 
        public static byte[] httpGet(string Uri)
        {
            byte[] arrs = null; 
            using (var httpClient = new HttpClient())
            {
                var response = httpClient.GetAsync(Uri).Result;

                if (response.IsSuccessStatusCode)
                    // 读取响应为 字节/流/字符串  
                    //response.Content.ReadAsStringAsync().ContinueWith(
                    //    (readTask) => Console.WriteLine(readTask.Result));
                    arrs = response.Content.ReadAsByteArrayAsync().Result;
            }
        
            return arrs;

        }
    }
}
