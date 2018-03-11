using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using System.Xml.Linq;

namespace ECB_WebAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            
            GlobalConfiguration.Configure(WebApiConfig.Register);
            string str = ConfigurationManager.ConnectionStrings["ECB_WebAPIContext"].ConnectionString;

            SqlConnection con = new SqlConnection(str);
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand cmd;
            string sql = null;

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
            con.Open();
            foreach (var result in cubes)
            {

                try
                {
                    sql = "INSERT INTO EC_BANK(id,Date,Currency,Rate) VALUES('" + result.Date.Year.ToString() + result.Date.Month.ToString() + result.Date.Day.ToString() + result.Currency.ToString() + "','" + result.Date.ToString() + "','" + result.Currency + "'," + result.Rate.ToString() + ")";
                    cmd = new SqlCommand(sql, con);
                    da.InsertCommand = cmd;
                    da.InsertCommand.ExecuteNonQuery();
                }
                catch
                {

                }
                
            }
            con.Close();

        }
    }
}
