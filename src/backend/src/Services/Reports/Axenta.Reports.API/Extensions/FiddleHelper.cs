namespace Axenta.Reports.API.Extensions;

public static class FiddleHelper
{
    /// <summary>
    ///     Assemble the connection string
    /// </summary>
    /// <param name="dataSource"></param>
    /// <param name="catalog"></param>
    /// <param name="id">user id</param>
    /// <param name="password">user password</param>
    /// <returns></returns>
    public static string GetConnectionStringSqlServer(string dataSource, string catalog, string id, string password)
    {
        return
            $"Data Source= {dataSource};Initial Catalog={catalog};Persist Security Info=True;User ID={id};Password={password};Encrypt=True;TrustServerCertificate=True;";
    }
}