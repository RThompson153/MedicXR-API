using Microsoft.Data.SqlClient;
using System.Data;

namespace MedicXR_API.Context
{
	internal static class SqlExtensions
	{
		internal static SqlParameter ToSqlParam(this object? value, string name, SqlDbType type, ParameterDirection direction = ParameterDirection.Input) => new(name, (value ?? DBNull.Value))
		{
			ParameterName = name,
			SqlDbType = type,
			Direction = direction
		};
	}
}
