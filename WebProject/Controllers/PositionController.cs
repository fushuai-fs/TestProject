using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;


namespace WebProject.Controllers
{
    public class PositionController : ApiController
    {
        [Authorize]
        // GET: api/Position
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Position/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Position
        public void Post([FromBody]Entity.Position position)
        {
            Business.Position.Save(position);
        }
        
        public void Post1()
        {
            if (!Request.Content.IsMimeMultipartContent("form-data"))
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }
 
             
        }
        // PUT: api/Position/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Position/5
        public void Delete(int id)
        {
        }


        [HttpPost]
        public async Task<HttpResponseMessage> PostFile()
        {  
            try
            {
                // 是否请求包含multipart/form-data。
                if (!Request.Content.IsMimeMultipartContent())
                { 
                    throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
                }

                string root = HttpContext.Current.Server.MapPath("/UploadFiles/");
                if (!Directory.Exists(HttpContext.Current.Server.MapPath("~/UploadFiles/")))
                {
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/UploadFiles/"));
                }

                var provider = new MultipartFormDataStreamProvider(root);

              //  StringBuilder sb = new StringBuilder(); // Holds the response body

                // 阅读表格数据并返回一个异步任务.
                await Request.Content.ReadAsMultipartAsync(provider);
                foreach (var str in provider.FormData)
                { 
                    string sss = provider.FormData.Get(str.ToString());
                     

                }
                // 如何上传文件到文件名.
                foreach (var file in provider.FileData)
                {

                }
                //{
                    //string orfilename = file.Headers.ContentDisposition.FileName.TrimStart('"').TrimEnd('"');
                    //FileInfo fileinfo = new FileInfo(file.LocalFileName); 
                    ////最大文件大小
                    ////int maxSize = Convert.ToInt32(SettingConfig.MaxSize);
                    //if (fileinfo.Length <= 0)
                    //{
                    //    json.Success = false;
                    //    json.Msg = "请选择上传文件";
                    //    json.Code = 301;
                    //}
                    //else if (fileinfo.Length > 100000)
                    //{
                    //    json.Msg = "上传文件大小超过限制";
                    //    json.Code = 302;
                    //}
                    //else
                    //{
                    //    string fileExt = orfilename.Substring(orfilename.LastIndexOf('.')); 
                    //}
                    //fileinfo.Delete();//删除原文件
                //}
            }
            catch (System.Exception e)
            { 
            } 
            return   new HttpResponseMessage(HttpStatusCode.InternalServerError);
        }

    }
 
}
