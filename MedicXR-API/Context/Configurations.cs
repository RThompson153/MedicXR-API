using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicXR_API.Context
{
    internal class Configurations<T> : IEntityTypeConfiguration<T> where T : class
	{
		public void Configure(EntityTypeBuilder<T> builder)
		{
			var type = typeof(T);

			foreach (var prop in type.GetProperties())
			{
				builder.Property(prop.Name);

				if (prop.GetCustomAttributes(typeof(NotMappedAttribute), false).Any())
					builder.Ignore(prop.Name);
			}
		}
	}
}
