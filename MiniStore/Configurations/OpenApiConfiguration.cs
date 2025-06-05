using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Any;
using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;

namespace MiniStore.Configurations
{
    public static class OpenApiConfiguration
    {
        public static void AddOpenApiWithCustomSchema(WebApplicationBuilder builder)
        {
            builder.Services.AddOpenApi(
                 options =>
                 {
                     options.AddSchemaTransformer((schema, context, cancellationToken) =>
                     {
                         if (context.JsonTypeInfo.Type.IsAssignableTo(typeof(IEnumerable))
                             && context.JsonTypeInfo.ElementType is not null
                             && context.JsonTypeInfo.ElementType.IsEnum)
                         {
                             var names = Enum.GetNames(context.JsonTypeInfo.ElementType);
                             schema.Format = $"{string.Join(" | ", names)}";
                         }

                         if (context.JsonTypeInfo.Type.IsEnum)
                         {
                             var names = Enum.GetNames(context.JsonTypeInfo.Type);
                             schema.Format = string.Join(" | ", names);
                             schema.Type = "Enum";
                             schema.Example = new OpenApiString(names.First());
                         }

                         if (context.JsonTypeInfo.Type == typeof(DateOnly))
                         {
                             schema.Type = "Date seule";
                             schema.Format = "yyyy-mm-dd";
                             schema.Example = new OpenApiDate(DateTime.Now);
                         }

                         return Task.CompletedTask;
                     });

                     options.AddDocumentTransformer((document, _, _) =>
                     {
                         document.Info.Title = "API MiniStore";
                         document.Info.Description = "API back office de l'application MiniStore.";
                         document.Info.Version = "v1";
                         return Task.CompletedTask;
                     });
                 });
        }
    }
}
