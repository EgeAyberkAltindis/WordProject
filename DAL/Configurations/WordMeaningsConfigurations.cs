using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Configurations
{
    public class WordMeaningsConfigurations : IEntityTypeConfiguration<WordMeaning>
    {
        public void Configure(EntityTypeBuilder<WordMeaning> builder)
        {
            builder.HasOne(x => x.TurkıshWord).WithMany(x => x.WordMeaning).HasForeignKey(x => x.TurkishWordId);
            builder.HasOne(x => x.EnglishWord).WithMany(x => x.WordMeaning).HasForeignKey(x => x.EnglisWordId);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
        }
    }
}
