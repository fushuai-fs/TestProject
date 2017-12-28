using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Web;
using System.Xml;
using WebProject.Models;

namespace WebProject.Formatters
{
    public class ProductCsvFormatter : BufferedMediaTypeFormatter
    {
        public ProductCsvFormatter()
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/csv"));
            SupportedEncodings.Add(new UTF8Encoding(encoderShouldEmitUTF8Identifier: false));
            SupportedEncodings.Add(Encoding.GetEncoding("iso-8859-1"));
        }

        public override bool CanReadType(Type type)
        {
            if (type == typeof(Product))
            {
                return true;
            }
            else
            {
                Type enumerableType = typeof(IEnumerable<Product>);
                return enumerableType.IsAssignableFrom(type);
            }
        }

        public override object ReadFromStream(Type type, Stream readStream, HttpContent content, IFormatterLogger formatterLogger)
        {
            using (StreamReader reader = new StreamReader(readStream))
            {
                /* Following code is not in good shape. In this code we make the basic plumbing to parse 
                 * the input "application/custom-product-type" format string and deserialize it to Product 
                 * objects.
                 */
                String productString = reader.ReadToEnd().ParseProductsString();
                String[] productArray = productString.Split(new string[] { "}{" }, StringSplitOptions.RemoveEmptyEntries);

                List<Product> products = new List<Product>();
                foreach (string s in productArray)
                {
                    String[] productInterim = s.Split(new char[] { ',' });
                    int _id = Convert.ToInt32(productInterim[0].Replace("\"", String.Empty));
                    string _name = productInterim[1].Replace("\"", String.Empty);
                    string _category = productInterim[2].Replace("\"", String.Empty);
                    decimal _price = Convert.ToDecimal(productInterim[3].Replace("\"", String.Empty));

                    products.Add(new Product()
                    {
                        Id = _id,
                        Name = _name,
                        Category = _category,
                        Price = _price,
                    });
                }
                return products;
            }

        }

        //指示格式化程序可以序列化的类型
        public override bool CanWriteType(Type type)
        {
            if (type == typeof(Product))
            {
                return true;
            }
            else
            {
                Type enumerableType = typeof(IEnumerable<Product>);
                return enumerableType.IsAssignableFrom(type);
            }
        }
        //public override void WriteToStream(Type type, object value, Stream writeStream, HttpContent content, CancellationToken cancellationToken)
        //{
        //    base.WriteToStream(type, value, writeStream, content, cancellationToken);
        //}
        public override void WriteToStream(Type type, object value, Stream writeStream, HttpContent content)
        {
            Encoding effectiveEncoding = SelectCharacterEncoding(content.Headers);

            using (var writer = new StreamWriter(writeStream, effectiveEncoding))
            {
                var products = value as IEnumerable<Product>;
                if (products != null)
                {
                    foreach (var product in products)
                    {
                        WriteItem(product, writer);
                    }
                }
                else
                {
                    var singleProduct = value as Product;
                    if (singleProduct == null)
                    {
                        throw new InvalidOperationException("Cannot serialize type");
                    }
                    WriteItem(singleProduct, writer);
                }
            }
        }
        private void WriteItem(Product product, StreamWriter writer)
        {
            writer.WriteLine("{0},{1},{2},{3}", Escape(product.Id),
                Escape(product.Name), Escape(product.Category), Escape(product.Price));
        }
        static char[] _specialChars = new char[] { ',', '\n', '\r', '"' };
        private string Escape(object o)
        {
            if (o == null)
            {
                return "";
            }
            string field = o.ToString();
            if (field.IndexOfAny(_specialChars) != -1)
            {
                return String.Format("\"{0}\"", field.Replace("\"", "\"\""));
            }
            else return field;
        }


    }
    public static class ExtensionMethods
    {
        public static string ParseProductsString(this string original)
        {
            return original.Replace("\r\n",String.Empty).Replace("][", "}{")
                           .Replace("[", string.Empty).Replace("]", string.Empty)
                           .TrimStart('{').TrimEnd('}');
        }

        public static string ReplaceExtraQuotes(this string original)
        {
            return original.Replace("\"", String.Empty);
        }
    }
}