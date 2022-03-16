using Application.Repositories;
using Infrastructure.Data;
using Infrastructure.Implementation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class ServiceCollectionExtensions
{

    public static void AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
        services.AddScoped<IAuthorRepository, AuthorRepository>();
        services.AddScoped<IQuoteRepository, QuoteRepository>();
    }
}
