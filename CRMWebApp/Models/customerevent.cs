using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CRMWebApp.Models
{
    [Table("customerEvent")]
    public class customerevent
    {
        public int contact_id { get; set; }
        public int event_id { get; set; }
    }
}
