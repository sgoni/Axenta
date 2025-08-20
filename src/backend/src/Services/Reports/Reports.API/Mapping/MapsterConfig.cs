namespace Axenta.Reports.API.Mapping;

public static class MapsterConfig
{
    public static void RegisterBalanceSheetMaping()
    {
        TypeAdapterConfig<IDictionary<string, object>, BalanceSheetDto>.NewConfig()
            .Map(dest => dest.AccountType, src => src["AccountType"])
            .Map(dest => dest.Balance, src => src["Balance"])
            .Map(dest => dest.Code, src => src["Code"])
            .Map(dest => dest.Name, src => src["Name"]);
    }
}