namespace Axenta.Reports.API.Mapping;

public static class MapsterConfig
{
    public static void RegisterBalanceSheetMapping()
    {
        TypeAdapterConfig<IDictionary<string, object>, BalanceSheetDto>.NewConfig()
            .Map(dest => dest.AccountType, src => src["AccountType"])
            .Map(dest => dest.Balance, src => src["Balance"])
            .Map(dest => dest.Code, src => src["Code"])
            .Map(dest => dest.Name, src => src["Name"]);
    }

    public static void RegisterGeneralLedgerMapping()
    {
        TypeAdapterConfig<IDictionary<string, object>, GeneralLedgerDto>.NewConfig()
            .Map(dest => dest.Credit, src => src["Credit"])
            .Map(dest => dest.Date, src => src["Date"])
            .Map(dest => dest.Debit, src => src["Debit"])
            .Map(dest => dest.Description, src => src["Description"])
            .Map(dest => dest.Movement, src => src["Movement"]);
    }

    public static void RegisterIncomeStatementMapping()
    {
        TypeAdapterConfig<IDictionary<string, object>, IncomeStatementDto>.NewConfig()
            .Map(dest => dest.Balance, src => src["Balance"])
            .Map(dest => dest.Code, src => src["Code"])
            .Map(dest => dest.Name, src => src["Name"]);
    }
}