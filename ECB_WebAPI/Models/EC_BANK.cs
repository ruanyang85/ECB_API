using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ECB_WebAPI.Models
{
    public class EC_BANK
    {
        public int id { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public string Currency { get; set; }
        public decimal Rate { get; set; }
    }
}