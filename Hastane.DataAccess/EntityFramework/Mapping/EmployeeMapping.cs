using Hastane.Entities.Concrete;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hastane.DataAccess.EntityFramework.Mapping
{
	public class EmployeeMapping:BaseEntityTypeConfig<Employee>
	{
		public override void Configure(EntityTypeBuilder<Employee> builder)
		{
			builder.HasKey(x => x.ID);
			builder.Property(x => x.ID).IsRequired(true);
			base.Configure(builder);
		}
	}
}
