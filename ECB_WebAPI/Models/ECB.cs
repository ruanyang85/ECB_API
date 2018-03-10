using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ECB_WebAPI.Models
{
    public class ECB
    {
        public string Base { get; set; }
        public Dictionary<string, decimal> rates { get; set; }
    }
}