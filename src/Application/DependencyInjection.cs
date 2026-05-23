using Application.Behaviors;
using Application.Commands.Estados;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(
            this IServiceCollection services)
        {
            // MediatR
            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(
                    typeof(CriarEstadoHandler).Assembly));

            // FluentValidation
            services.AddValidatorsFromAssembly(
                typeof(CriarEstadoCommandValidator).Assembly);

            // Pipeline Behavior
            services.AddTransient(
                typeof(IPipelineBehavior<,>),
                typeof(ValidationBehavior<,>));

            return services;
        }
    }
}
