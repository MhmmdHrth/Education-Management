using Microsoft.EntityFrameworkCore;
using Student_Management.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Student_Management.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Department> Departments { get; set; }

        public DbSet<Teacher> Teachers { get; set; }

        public DbSet<Student> Students { get; set; }
    }
}
