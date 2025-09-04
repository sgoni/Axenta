namespace Accounting.Domain.Enums;

public class JournalEntryType : Enumeration
{
    public static SourceType Normal = new(0, nameof(Normal)); // Normal accounting seat
    public static SourceType Adjustment = new(1, nameof(Adjustment)); // Adjustment
    public static SourceType Accrual = new(2, nameof(Accrual)); // accrued
    public static SourceType Reversal = new(3, nameof(Reversal)); // Reversa of a previous seat
    public static SourceType Reconciliation = new(4, nameof(Reconciliation)); // Reconciliation

    public static SourceType
        ReconciliationAdjustment = new(5, nameof(ReconciliationAdjustment)); // Reconciliation adjustment

    public static SourceType ReconciliationAccrual = new(6, nameof(ReconciliationAccrual)); // Reconciliation accrual
    public static SourceType ReconciliationReversal = new(7, nameof(ReconciliationReversal)); // Reconciliation reversal
    public static SourceType Draft = new(8, nameof(Draft)); // Draft
    public static SourceType Closing = new(9, nameof(Closing)); // Accounting closure (eg, end of period)   

    public JournalEntryType(int id, string name) : base(id, name)
    {
    }
}