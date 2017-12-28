using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Http;
using WebProject.Models;

namespace WebProject.Controllers
{
    //https://docs.microsoft.com/en-us/aspnet/web-api/overview/formats-and-model-binding/model-validation-in-aspnet-web-api
    /// <summary>
    /// 
    /// </summary>
    public class DefaultController : ApiController
    {
        Product[] products = new Product[]
       {
            new Product { Id = 1, Name = "Tomato Soup", Category = "Groceries", Price = 1 },
            new Product { Id = 2, Name = "Yo-yo", Category = "Toys", Price = 3.75M },
            new Product { Id = 3, Name = "Hammer", Category = "Hardware", Price = 16.99M }
       };

        public IEnumerable<Product> GetAllProducts()
        {
            return products;
        }
        [HttpGet]
        [HttpPost]
        public IHttpActionResult Product(int id)
        {
            var product = products.FirstOrDefault((p) => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        /// <summary>
        /// 从请求body里读取参数
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost]
        public IEnumerable<Product> Post([FromBody]Product product)
        {
            //  int test = Convert.ToInt32(product); 异常测试
            List<Product> list = new List<Models.Product>();
            list.Add(product);
            product.Id = (product.Id++);
            list.Add(product);
            return list;

        }
        /// <summary>
        /// 从URI读取 http://localhost:50276/api/Default/gets?Id=2&Name=fus&Category=chi&Price=100
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public IEnumerable<Product> gets([FromUri]Product product)
        {
            //  int test = Convert.ToInt32(product); 异常测试
            List<Product> list = new List<Models.Product>();
            list.Add(product);
            product.Id = (product.Id++);
            list.Add(product);
            return list;

        }
        public IEnumerable<Product> posts([FromUri]Product product)
        {
            //  int test = Convert.ToInt32(product); 异常测试
            List<Product> list = new List<Models.Product>();
            list.Add(product);
            product.Id = (product.Id++);
            list.Add(product);
            return list;

        }

        public HttpResponseMessage Get()
        {
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, "");
            response.Content = new StringContent("hello", Encoding.Unicode);
            //response.Headers.CacheControl = new CacheControlHeaderValue()
            //{
            //    MaxAge = TimeSpan.FromMinutes(20)
            //};
            return response;
        }

        public HttpResponseMessage GetDB()
        {
            // Get a list of products from a database.
            //IEnumerable<Product> products = GetProductsFromDB();

            // Write the list to the response body.
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, products);
            return response;
        }


        // Request：
        // http：// localhost：9445 / api / MediaFormatters 
        // Accept：text / csv 

        // Response    
        // 1，BMW，Race，99999999 
        public HttpResponseMessage GetCvs()
        {
            var product = new Product { Id = 1, Name = "BMW", Category = "Race", Price = 99999999 };
            var response = Request.CreateResponse(HttpStatusCode.OK, product);
            return response;
        }

        [HttpPost]
        public HttpResponseMessage PostCvs(List<Product> p)
        {
            if (p == null)
                return new HttpResponseMessage(HttpStatusCode.BadRequest);

            List<Product> _products = new List<Models.Product>();
            _products.AddRange(p);
            _products.AddRange(p);

            return Request.CreateResponse(HttpStatusCode.Created, _products);
        }
    }
}
