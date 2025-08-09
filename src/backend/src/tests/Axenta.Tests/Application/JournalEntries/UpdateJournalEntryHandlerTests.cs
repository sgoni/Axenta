namespace Axenta.Tests.Application;

public class UpdateJournalEntryHandlerTests
{
    [Fact]
    public async Task Handler_updates_entry_and_saves()
    {
        // Mock contexto in-memory con un JournalEntry ya existente
        // Ejecuta Handle(...) con un command válido
        // Verifica que SaveChangesAsync fue llamado
        // Verificar que cambios fueron aplicados correctamente
    }

    [Fact]
    public async Task Handler_returns_error_when_unbalanced()
    {
        // Simula command con líneas no balanceadas
        // Verifica que result.Success == false y error message
    }
}