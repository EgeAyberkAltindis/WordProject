using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Configurations
{

    public class TurkishWordConfigurations : IEntityTypeConfiguration<TurkıshWord>
    {
        public void Configure(EntityTypeBuilder<TurkıshWord> builder)
        {
            throw new NotImplementedException();
        }
    }
}
