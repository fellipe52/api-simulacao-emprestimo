using Infrastructure.Repositories;
using Infrastructure.Repositories.Interfaces;
using MongoDB.Driver;
using UseCase.UseCase.Interfaces;
using UseCase.UseCase;

namespace WebApi.Extensions;

internal static class InfrastructureExtension
{
    private static string ConnectionString;

    static InfrastructureExtension()
    {
    }

    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connection)
    {
        ConnectionString += connection;

        return services
            .AddSqlRepositories();
    }

    private static IServiceCollection AddSqlRepositories(this IServiceCollection services)
    {
        return services
        .AddScoped<IPaymentFlowSummaryRepository>(provider => new PaymentFlowSummaryRepository(ConnectionString))
        .AddScoped<IPropostaRepository>(provider => new PropostaRepository(ConnectionString));

    }

    private static IServiceCollection AddClients(this IServiceCollection services)
    {
        return services.AddSingleton<IMongoClient>(s =>
            new MongoClient(ConnectionString));
    }
}