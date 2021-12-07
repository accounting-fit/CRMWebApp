using Castle.Components.DictionaryAdapter;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CRMWebApp.Models
{
    [Table("contacts")]
    public class contacts {
        public int contact_id { get; set; }
        public string fname { get; set; }
        public string lname { get; set; }
        public string mname { get; set; }
        public string email { get; set; }
        public string mobile { get; set; }
        public string phone { get; set; }
        public string website { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public bool type { get; set; }
        public string des { get; set; }
        public string other { get; set; }

    }
}
