using Hastane.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hastane.DataAccess.EntityFramework.Mapping
{
    public class ManagerMapping:BaseEntityTypeConfig<Manager>
    {
        public override void Configure(EntityTypeBuilder<Manager> builder)
        {
            builder.HasKey(z => z.ID);

            builder.HasMany(x => x.personels)
                .WithOne(x => x.Manager)
                .HasForeignKey(x => x.ManagerID)
                .OnDelete(DeleteBehavior.Restrict);
            base.Configure(builder);
        }
    }
}
