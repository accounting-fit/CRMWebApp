using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CRMWebApp.Models
{

    [Table("events")]
    public class events
    {
        public int event_id { get; set; }
        public string topic { get; set; }
        public string type { get; set; }
        public string status { get; set; }
        public string des { get; set; }
        public string start_date { get; set; }
        public string start_time { get; set; }
        public string end_date { get; set; }
        public string end_time { get; set; }
        [NotMapped]
        public int? contact_id { get; set; }
        [NotMapped]
        public string contact_name { get; set; }

    }
}
