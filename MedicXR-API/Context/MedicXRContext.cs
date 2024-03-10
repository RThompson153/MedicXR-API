using MedicXR_API.Context.Models;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace MedicXR_API.Context
{
	public partial class MedicXRContext : DbContext
	{
		private ModelBuilder _modelBuilder;
		private string _schema;

		internal protected virtual DbSet<Illness> Illnesses { get; set; }

		public MedicXRContext(DbContextOptions options) : base(options)
		{
			_schema = "medar";
		}
		public MedicXRContext(string ctx) : base(getOptions(ctx))
		{
			_schema = "medar";
		}

		protected override void OnModelCreating(ModelBuilder mb)
		{
			_modelBuilder = mb;
			_modelBuilder.HasDefaultSchema(SqlConstants.Schema);

			configureDbSet<Illness>();
		}

		private static DbContextOptions getOptions(string ctx) => SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder(), ctx).Options;
		private void configureDbSet<T>() where T : class => new Configurations<T>().Configure(_modelBuilder.Entity<T>());
		private void configureViewOrTable<T>(string name, bool? isView = false) where T : class
		{
			var config = new Configurations<T>();

			_modelBuilder.Entity<T>(entity =>
			{
				if (isView == true)
					entity.ToView(name);
				else
					entity.ToTable(name);

				config.Configure(entity);
			});
		}

		public async Task<IEnumerable<Illness>> GetIllnesses() => await Illnesses.FromSqlRaw($"EXEC {_schema}.{SqlConstants.sp_GetIllnesses}").ToListAsync();
	}
}
