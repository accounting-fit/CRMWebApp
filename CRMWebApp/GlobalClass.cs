using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRMWebApp.GlobalElemnts
{
    public static class GlobalClass
    {
        public static string ConnectionString { get; set; } = @"data source=SAYED-PC;initial catalog=CRMDB;persist security info=True;user id=sa;password=abu123;";
        
    }
}
