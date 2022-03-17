using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FilmesPV.Models;

namespace FilmesPV.Data
{
    public class FilmesPVContext : DbContext
    {
        public FilmesPVContext (DbContextOptions<FilmesPVContext> options)
            : base(options)
        {
        }

        public DbSet<FilmesPV.Models.Filme> Filme { get; set; }
    }
}
