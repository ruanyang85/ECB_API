namespace ECB_WebAPI.Migrations
{
    using System;
    using System.Configuration;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Data.SqlClient;
    using System.Globalization;
    using System.Linq;
    using System.Xml.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ECB_WebAPI.Models.ECB_WebAPIContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ECB_WebAPI.Models.ECB_WebAPIContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            string str = ConfigurationManager.ConnectionStrings["ECB_WebAPIContext"].ConnectionString;

            string url = "https://www.ecb.europa.eu/stats/eurofxref/eurofxref-hist-90d.xml ";
            XDocument doc = XDocument.Load(url);
            XNamespace gesmes = "http://www.gesmes.org/xml/2002-08-01";
            XNamespace ns = "http://www.ecb.int/vocabulary/2002-08-01/eurofxref";

            var cubes = doc.Descendants(ns + "Cube")
                           .Where(x => x.Attribute("currency") != null)
                           .Select(x => new {
                               Date = DateTime.ParseExact(x.Parent.Attribute("time").Value, "yyyy-MM-dd", CultureInfo.InvariantCulture),
                               Currency = (string)x.Attribute("currency"),
                               Rate = (decimal)x.Attribute("rate")
                           });
      
            foreach (var result in cubes)
            {
                try
                {
                   

                    context.EC_BANK.AddOrUpdate(new Models.EC_BANK() {
                        Date = result.Date, Currency= result.Currency,Rate = result.Rate
                    });

                }
                catch
                {
              
                }
            }
           
       

        }
    }
}
