namespace MedicXR_API.Context
{
    internal static class SqlConstants
    {
        internal const string DefaultSchema = "medxr";
        internal const string MedARSchema = "medar";
        internal const string EXEC = "EXEC";
        internal const string P0 = "p0";
        internal const string P1 = "p1";
        internal const string P2 = "p2";

        internal const string sp_GetClient = "GetClient @ClientId=@p0, @ClientSecret=@p1";
        internal const string sp_GetUsers = "GetUsers @ClientId=@p0, @UserIds=@p1";
        internal const string sp_GetIllnesses = "GetIllnesses @ClientId=@p0";
    }
}
