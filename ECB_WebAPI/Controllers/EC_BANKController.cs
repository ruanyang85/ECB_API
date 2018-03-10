using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Remoting.Contexts;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Script.Serialization;
using ECB_WebAPI.Models;

namespace ECB_WebAPI.Controllers
{
    [RoutePrefix("rates")]
    public class EC_BANKController : ApiController
    {
        string str = ConfigurationManager.ConnectionStrings["ECB_WebAPIContext"].ConnectionString;
        private ECB_WebAPIContext db = new ECB_WebAPIContext();

        // GET: api/EC_BANK
        public IQueryable<EC_BANK> GetEC_BANK()
        {
            return db.EC_BANK;
        }
      

        [Route("{date:datetime}")]
        [ResponseType(typeof(ECB))]
        public async Task<IHttpActionResult> GetByDate(DateTime date)
        {
            List<ECB> listLatest = new List<ECB>();
            using (SqlConnection con = new SqlConnection(str))
            {
                String latestSQL = "SELECT * FROM EC_BANK WHERE DATE= '" + date.ToString("yyyy-MM-dd") + "' ORDER BY Currency";
                SqlCommand cmd = new SqlCommand(latestSQL, con);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                con.Open();
                SqlDataReader rdr = da.SelectCommand.ExecuteReader();

                ECB latest = new ECB();
                latest.Base = "EUR";
                latest.rates = new Dictionary<string, decimal>();
                while (rdr.Read())
                {
                   latest.rates.Add(rdr["Currency"].ToString(), Convert.ToDecimal(rdr["Rate"]));
                };

                listLatest.Add(latest);
                con.Close();
            }

            return Ok(listLatest);
        }


        [Route("analyze")]
        [HttpGet]
        public IEnumerable<ECB_Analyze> Analyze()
        {
            List<ECB_Analyze> listLatest = new List<ECB_Analyze>();
            using (SqlConnection con = new SqlConnection(str))
            {
                String latestSQL = "SELECT Currency,MIN(Rate) AS minRate,MAX(Rate) AS MaxRate,AVG(Rate) as AvgRate FROM EC_BANK  group BY Currency ORDER BY Currency";
                SqlCommand cmd = new SqlCommand(latestSQL, con);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                con.Open();
                SqlDataReader rdr = da.SelectCommand.ExecuteReader();

                ECB_Analyze latest = new ECB_Analyze();
                latest.Base = "EUR";
                latest.rates_analyze = new Dictionary<string, Dictionary<string, decimal>>();
                while (rdr.Read())
                {

                    Dictionary<string, decimal> d = new Dictionary<string, decimal>()
                    {
                        {"min", Convert.ToDecimal(rdr["minRate"])},
                        {"max", Convert.ToDecimal(rdr["MaxRate"])},
                        {"avg", Convert.ToDecimal(rdr["AvgRate"])},

                    };
                    latest.rates_analyze.Add(rdr["Currency"].ToString(), d);
                };

                listLatest.Add(latest);
                con.Close();
            }

            return listLatest;


        }

        [Route("latest")]
        [HttpGet]
        public IEnumerable<ECB> latest()
        {
            List<ECB> listLatest = new List<ECB>();
            using (SqlConnection con = new SqlConnection(str))
            {
                String latestSQL = "SELECT * FROM EC_BANK WHERE DATE= (SELECT MAX(Date) FROM EC_BANK) ORDER BY Currency";
                SqlCommand cmd = new SqlCommand(latestSQL, con);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                con.Open();
                SqlDataReader rdr = da.SelectCommand.ExecuteReader();

                ECB latest = new ECB();
                latest.Base = "EUR";
                latest.rates = new Dictionary<string, decimal>();
                while (rdr.Read())
                {
                    latest.rates.Add(rdr["Currency"].ToString(), Convert.ToDecimal(rdr["Rate"]));
                };

                listLatest.Add(latest);
                con.Close();
            }

            return listLatest;
        }
        // GET: api/EC_BANK/5
        [ResponseType(typeof(EC_BANK))]
        public async Task<IHttpActionResult> GetEC_BANK(int id)
        {
            EC_BANK eC_BANK = await db.EC_BANK.FindAsync(id);
            if (eC_BANK == null)
            {
                return NotFound();
            }

            return Ok(eC_BANK);
        }

        // PUT: api/EC_BANK/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutEC_BANK(int id, EC_BANK eC_BANK)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != eC_BANK.id)
            {
                return BadRequest();
            }

            db.Entry(eC_BANK).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EC_BANKExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/EC_BANK
        [ResponseType(typeof(EC_BANK))]
        public async Task<IHttpActionResult> PostEC_BANK(EC_BANK eC_BANK)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.EC_BANK.Add(eC_BANK);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = eC_BANK.id }, eC_BANK);
        }

        // DELETE: api/EC_BANK/5
        [ResponseType(typeof(EC_BANK))]
        public async Task<IHttpActionResult> DeleteEC_BANK(int id)
        {
            EC_BANK eC_BANK = await db.EC_BANK.FindAsync(id);
            if (eC_BANK == null)
            {
                return NotFound();
            }

            db.EC_BANK.Remove(eC_BANK);
            await db.SaveChangesAsync();

            return Ok(eC_BANK);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EC_BANKExists(int id)
        {
            return db.EC_BANK.Count(e => e.id == id) > 0;
        }
    }
}