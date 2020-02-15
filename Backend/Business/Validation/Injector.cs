using Business.Validation.Validators;
using Domain;
using Domain.Dtos;
using Domain.Entities;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace Business.Validation
{
    public static class Injector
    {
        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddTransient<IValidator<CredentialDto>, CredentialValidator>();
            services.AddTransient<IValidator<User>, RegisterUserValidator>();

            services.AddTransient<IValidatorInterceptor, ValidationInterceptor>();

            return services;
        }
    }
}