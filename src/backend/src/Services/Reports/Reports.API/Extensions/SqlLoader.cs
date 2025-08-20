namespace Axenta.Reports.API.Extensions;

public static class SqlLoader
{
    public static string ProjectName()
    {
        return Assembly.GetExecutingAssembly().GetName().Name;
    }

    public static string Load(string relativePath)
    {
        var basePath = AppContext.BaseDirectory;
        var fullPath = Path.Combine(basePath, "Sql", relativePath);

        if (!File.Exists(fullPath)) throw new FileNotFoundException("SQL file not found: ", fullPath);
        return File.ReadAllText(fullPath);
    }

    public static string LoadSql(string resourceName)
    {
        var assembly = Assembly.GetExecutingAssembly();
        using var stream = assembly.GetManifestResourceStream(resourceName);
        using var reader = new StreamReader(stream);
        return reader.ReadToEnd();
    }
}