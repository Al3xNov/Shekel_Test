using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;

namespace TestWebApi.Tests;
public class DatabaseFixture : IAsyncLifetime
{
    public MsSqlTestcontainer MsSqlContainer { get; }
    public DatabaseFixture()
    {
        var mssqlConfiguration = new MsSqlTestcontainerConfiguration() { Password = "Q1w@e345678" };
        MsSqlContainer = new TestcontainersBuilder<MsSqlTestcontainer>()
            .WithDatabase(mssqlConfiguration)
            .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
            .WithCleanUp(true)
            .Build();
    }
    public async Task InitializeAsync()
    {
        if (MsSqlContainer is not null)
            await MsSqlContainer.StartAsync().ConfigureAwait(false);
    }

    public async Task DisposeAsync()
    {
        if (MsSqlContainer is not null)
            await MsSqlContainer.DisposeAsync().ConfigureAwait(false);
    }
}