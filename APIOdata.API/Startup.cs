using APIOdata.API.Model;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIOdata.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {


            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(Configuration["ConnStr"]);
            });
            services.AddOData();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            var builder = new ODataConventionModelBuilder();

            builder.EntitySet<Category>("Categories");

            builder.EntitySet<Product>("Products");

            #region Action

            //parametre alýrsa bu
            builder.EntityType<Category>().Action("TotalProductPrice").Returns<int>();

            builder.EntityType<Category>().Collection.Action("TotalProductPrice2").Returns<int>();

            //odata/categories/totalproductprice bodye eklenecek pametre

            builder.EntityType<Category>().Collection.Action("TotalProductPriceWithParameter").Returns<int>().Parameter<int>("categoryId");


            var actionTotal = builder.EntityType<Category>().Collection.Action("Total").Returns<int>();

            actionTotal.Parameter<int>("a");
            actionTotal.Parameter<int>("b");
            actionTotal.Parameter<int>("c");


            #endregion


            #region Function

            builder.EntityType<Category>().Collection.Function("CategoryCount").Returns<int>();


           var multiplyFunction =  builder.EntityType<Product>().Collection.Function("MultiplyFunction").Returns<int>();

           multiplyFunction.Parameter<int>("a1");
           multiplyFunction.Parameter<int>("a2");
           multiplyFunction.Parameter<int>("a3");

            #endregion


            builder.Function("GetKdv").Returns<int>();


            //complex type
            builder.EntityType<Product>().Collection.Action("LoginUser").Returns<string>().Parameter<Login>("UserLogin");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.OrderBy().MaxTop(null).SkipToken().Count().Filter();
                endpoints.Select().Expand();
                endpoints.MapODataRoute("odata", "odata", builder.GetEdmModel());
                endpoints.MapControllers();
            });
        }
    }
}
