

using BuberDinner.Api.Common.Errors;
using BuberDinner.Api.Common.Mapping;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace BuberDinner.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddControllers();
            //via exception filter attribute
            //builder.Services.AddControllers(options => options.Filters.Add<ErrorHandlingFilteAttribute>());

            services.AddSingleton<ProblemDetailsFactory, BuberDinnerProblemDetailsFactory>();

            services.AddMppings();
            return services;
        }
    }
}
