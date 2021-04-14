using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MusicProject.Models
{
    public class UploadContext : DbContext
    {
        public UploadContext(DbContextOptions<UploadContext> options) : base(options)
        {
        }
        public DbSet<Files> Files
        {
            get; set;
        }
    }
}
