using MedicXR_API.Context.Models;
using MedicXR_API.Globals.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace MedicXR_API.Context
{
    public partial class MedicXRContext : DbContext
	{
		private ModelBuilder _modelBuilder;
		public string Schema { get; set; }

		internal protected virtual DbSet<Client> Client { get; set; }
		internal protected virtual DbSet<User> Users { get; set; }
		internal protected virtual DbSet<Illness> Illnesses { get; set; }

		public MedicXRContext(DbContextOptions options) : base(options)
		{
			Schema = SqlConstants.DefaultSchema;
		}
		public MedicXRContext(string ctx) : base(getOptions(ctx))
		{
			Schema = SqlConstants.DefaultSchema;
		}

		protected override void OnModelCreating(ModelBuilder mb)
		{
			_modelBuilder = mb;
			_modelBuilder.HasDefaultSchema(SqlConstants.DefaultSchema);

			configureDbSet<Client>();
			configureDbSet<User>();
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

		public async Task<IEnumerable<Client>?> AuthenticateClient(string clientId, string clientSecret)
		{
			SqlParameter[] parameters = new SqlParameter[2]
			{
				clientId.ToSqlParam(SqlConstants.P0, SqlDbType.NVarChar),
				clientSecret.ToSqlParam(SqlConstants.P1, SqlDbType.NVarChar)
			};

			var clients = await Client.FromSqlRaw($"{SqlConstants.EXEC} {Schema}.{SqlConstants.sp_GetClient}", parameters).ToListAsync();

			return clients;
		}

		public async Task<IEnumerable<User>> GetUsers(string clientId, string userIds)
		{
			SqlParameter[] parameters = new SqlParameter[2]
			{
				clientId.ToSqlParam(SqlConstants.P0, SqlDbType.NVarChar),
				userIds.ToSqlParam(SqlConstants.P1, SqlDbType.NVarChar),
			};

			return await Users.FromSqlRaw($"{SqlConstants.EXEC} {Schema}.{SqlConstants.sp_GetUsers}", parameters).ToListAsync();
		}
	
		public async Task<IEnumerable<Illness>> GetIllnesses(string clientId) => await Illnesses.FromSqlRaw($"{SqlConstants.EXEC} {Schema}.{SqlConstants.sp_GetIllnesses}", clientId.ToSqlParam(SqlConstants.P0, SqlDbType.NVarChar)).ToListAsync();
	}
}
