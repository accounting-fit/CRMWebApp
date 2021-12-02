using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRMWebApp.Models
{
    public class leads
    {
        public int lead_id { get; set; }
        public string fname { get; set; }
        public string lname { get; set; }
        public string mname { get; set; }
        public string sales_person { get; set; }
        public string dep { get; set; }
        public string comp { get; set; }
        public string industry { get; set; }
        public string lead_source { get; set; }
        public string lead_status { get; set; }
        public int? no_empl { get; set; }
        public decimal? revenue { get; set; }
        public string des { get; set; }
        public string referred { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string email { get; set; }
        public string mobile { get; set; }
        public string phone { get; set; }
        public string website { get; set; }
        public string other { get; set; }

    }
}
