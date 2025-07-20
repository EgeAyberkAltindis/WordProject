using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Model.Entities;
using Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Configurations
{
    public class EnglishSentenceConfigurations : IEntityTypeConfiguration<EnglishSentence>
    {
        public void Configure(EntityTypeBuilder<EnglishSentence> builder)
        {

            //builder.HasKey(x => new { x.SentenceId, x.EnglishWordId});

            //builder.Property(x=>x.EnglishWordId).ValueGeneratedNever();
            //builder.Property(x=>x.SentenceId).ValueGeneratedNever();

            //builder.HasOne(x => x.Sentence).WithMany(x => x.EnglishSentence).HasForeignKey(x => x.EnglishWordId);
            //builder.HasOne(x => x.EnglishWord).WithMany(x => x.EnglishSentence).HasForeignKey(x => x.SentenceId);

            builder.Ignore(x => x.Id);

            builder.HasKey(x => new { x.EnglishWordId, x.SentenceId });

            builder.HasOne(x => x.EnglishWord)
                 .WithMany(x => x.EnglishSentence)
                 .HasForeignKey(x => x.EnglishWordId)
                .OnDelete(DeleteBehavior.Cascade);

             builder.HasOne(x => x.Sentence)
            .WithMany(x => x.EnglishSentence)
            .HasForeignKey(x => x.SentenceId)
                .   OnDelete(DeleteBehavior.Cascade);
        }
    }
}
