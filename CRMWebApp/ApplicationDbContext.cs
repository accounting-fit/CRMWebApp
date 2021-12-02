using CRMWebApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRMWebApp
{

    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }


    }

    //public class BusinessDbContext : DbContext
    //{
    //    public BusinessDbContext(DbContextOptions<BusinessDbContext> options) : base(options)
    //    {

    //    }

    //    public DbSet<contacts> contacts { get; set; }
    //    public DbSet<tasks> tasks { get; set; }
    //    public DbSet<events> events { get; set; }
       
    //}
}
