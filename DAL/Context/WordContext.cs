using DAL.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Model.Entities;
using Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Context
{
    public class WordContext: DbContext
    {
         public WordContext(DbContextOptions<WordContext> options): base (options)
        {
             
        }
        public WordContext()
        {
            
        }

        public DbSet<EnglishSentence> EnglishSentences { get; set; }
        public DbSet<EnglishWord> EnglishWords { get; set; }

        public DbSet<Sentence> Sentences { get; set; }

        public DbSet<TurkıshWord> TurkıshWords { get; set; }

        public DbSet<WordMeaning> WordMeanings { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured) optionsBuilder.UseSqlServer("server=DESKTOP-ABTB3OG\\SQLEXPRESS;Database=WordListDB;Trusted_Connection=True;TrustServerCertificate=true");
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new EnglishSentenceConfigurations());
            modelBuilder.ApplyConfiguration(new WordMeaningsConfigurations());
          


            base.OnModelCreating(modelBuilder);
        }
    }
    
}
