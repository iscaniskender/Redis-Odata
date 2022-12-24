using APIOdata.API.Model;
using Default;

var serviceRoot = "https://localhost:44383/odata";

var context = new Container(new Uri(serviceRoot));

//var products = context.Products.ExecuteAsync().Result;

//foreach (var product in products.ToList())
//{
//    Console.WriteLine(product.Name);
//}


//var categories = context.Categories.AddQueryOption("$filter", "Id eq 2").ExecuteAsync().Result.FirstOrDefault();
//Console.WriteLine(categories.Name);


//var products = context.Products.Expand(x => x.Category);

//products.ToList().ForEach(x => Console.WriteLine(x.Name + "- Category Name: " +x.Category.Name));


var categories = context.Categories.AddQueryOption("$select", "Name").ExecuteAsync().Result;

categories.ToList().ForEach(category =>
    Console.WriteLine(category.Name));
