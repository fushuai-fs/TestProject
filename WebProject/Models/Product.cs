using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// 数据验证
// action 中使用if (ModelState.IsValid) {执行操作}else {返回错误请求400错误}
using System.ComponentModel.DataAnnotations;

namespace WebProject.Models
{
    public class Product
    {
        [Required]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        [Required]
        public decimal Price { get; set; }
    }
}