using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;

namespace APIOdata.API.Model
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Stock { get; set; }
        public int Price { get; set; }
        public DateTime?  CreatedDate { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
