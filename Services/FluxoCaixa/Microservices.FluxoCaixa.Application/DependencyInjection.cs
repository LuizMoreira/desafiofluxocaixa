using AutoMapper;
using FluentValidation;
using MediatR;
using Microservices.FluxoCaixa.Application.Behaviours;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Microservices.FluxoCaixa.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            return services;
        }
    }
}
