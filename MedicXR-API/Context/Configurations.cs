using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace MedicXR_API.Context
{
    internal class Configurations<T> : IEntityTypeConfiguration<T> where T : class
	{
		public void Configure(EntityTypeBuilder<T> builder)
		{
			var type = typeof(T);

			foreach (var prop in type.GetProperties())
				builder.Property(prop.Name);
		}
	}
}
