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
        public Char id { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public string Currency { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        public double Rate { get; set; }


    
    }
}