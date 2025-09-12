using VaultSharp.V1.SecretsEngines;

namespace Axenta.BuildingBlocks.Secrets;

public interface ISecretManager
{
    Task<T> GetCredential<T>(string path) where T : new();
    Task<UsernamePasswordCredentials> GetPostgreSQLCredential<T>() where T : new();
    Task<UsernamePasswordCredentials> GetRabbitMQCredential<T>(string path) where T : new();
}