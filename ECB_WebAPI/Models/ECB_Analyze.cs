using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ECB_WebAPI.Models
{
    public class ECB_Analyze
    {
        public string Base { get; set; }
        public Dictionary<string, Dictionary<string, decimal>> rates_analyze { get; set; }
    }
}