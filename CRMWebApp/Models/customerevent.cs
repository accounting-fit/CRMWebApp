﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CRMWebApp.Models
{
    [Table("customers")]
    public class customer
    {
        public int contact_id { get; set; }
        public int support_id { get; set; }
        public string supportername { get; set; }
    }
    
}
