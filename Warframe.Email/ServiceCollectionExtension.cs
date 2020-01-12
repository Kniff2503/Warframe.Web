// ###############################################################
// Thomas Heise
// Warframe.Email
// ServiceCollectionExtension.cs
// 2019/12/23/09:13
// ###############################################################

using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Warframe.Email.Models;
using Warframe.Email.Services;
using Microsoft.Extensions.Configuration;
using Warframe.Email.Providers;

namespace Warframe.Email
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddEmailSender(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureApplicationCookie(o =>
            {
                o.ExpireTimeSpan = TimeSpan.FromDays(5);
                o.SlidingExpiration = true;
            });

            services.Configure<DataProtectionTokenProviderOptions>(o =>
                o.TokenLifespan = TimeSpan.FromHours(3));

            services.AddTransient<CustomEmailConfirmationTokenProvider<IdentityUser>>();
            services.AddTransient<IEmailSender, EmailSender>();
            services.Configure<AuthMessageSenderOptions>(configuration);

            return services;
        }
    }
}