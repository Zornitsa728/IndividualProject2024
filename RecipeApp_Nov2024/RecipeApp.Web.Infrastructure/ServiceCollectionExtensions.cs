using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RecipeApp.Data;
using RecipeApp.Data.Models;
using RecipeApp.Data.Repository.Interfaces;
using System.Reflection;

namespace RecipeApp.Web.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterRepositories(this IServiceCollection services, Assembly modelsAssembly, RecipeDbContext dbContext)
        {
            Type[] typesToExclude = new Type[] { typeof(ApplicationUser) };
            Type[] modelTypes = modelsAssembly
                .GetTypes()
                .Where(t => !t.IsAbstract &&
                    !t.IsInterface &&
                    !t.IsEnum &&
                    !t.Name.ToLower().EndsWith("attribute"))
                .ToArray();
            foreach (Type modelType in modelTypes)
            {
                if (typesToExclude.Contains(modelType))
                {
                    continue;
                }

                // Check if the model has a primary key
                var entityType = dbContext.Model.FindEntityType(modelType);

                var primaryKey = entityType.FindPrimaryKey();

                // Use `object` if there are multiple key properties (composite key)
                Type idType = primaryKey.Properties.Count == 1
                    ? primaryKey.Properties[0].ClrType
                    : typeof(object);

                Type repositoryInterface = typeof(IRepository<,>);
                Type repositoryInstanceType = typeof(BaseRepository<,>);

                Type[] typeArguments = { modelType, idType };

                repositoryInterface = repositoryInterface.MakeGenericType(typeArguments);
                repositoryInstanceType = repositoryInstanceType.MakeGenericType(typeArguments);

                services.AddScoped(repositoryInterface, repositoryInstanceType);
            }
        }
    }
}
