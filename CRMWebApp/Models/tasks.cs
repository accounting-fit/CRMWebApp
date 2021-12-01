using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CRMWebApp.Models
{

    [Table("tasks")]
    public class tasks
    {
        public int task_id { get; set; }
        public string task_name { get; set; }
        public string status { get; set; }
        public string refer_type { get; set; }
        public string refer_name { get; set; }
        public string priority { get; set; }
        public string des { get; set; }

    }
}
