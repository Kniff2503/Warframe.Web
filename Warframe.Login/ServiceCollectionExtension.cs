// ###############################################################
// Thomas Heise
// Warframe.Login
// ServiceCollectionExtension.cs
// 2020/01/12/15:44
// ###############################################################

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Warframe.Login
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddLogin(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
                {
                    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
                })
                .AddCookie()
                .AddFacebook(
                    options =>
                    {
                        options.AppId = configuration["Authentication:Facebook:AppId"];
                        options.AppSecret = configuration["Authentication:Facebook:AppSecret"];
                    })
                .AddGoogle(
                    options =>
                    {
                        IConfigurationSection googleAuthNSection = configuration.GetSection("Authentication:Google");
                        options.ClientId = googleAuthNSection["AppId"];
                        options.ClientSecret = googleAuthNSection["AppSecret"];
                    })
                .AddMicrosoftAccount(options =>
                {
                    options.ClientId = configuration["Authentication:Microsoft:AppId"];
                    options.ClientSecret = configuration["Authentication:Microsoft:AppSecret"];
                })
                .AddTwitter(options =>
                {
                    options.ConsumerKey = configuration["Authentication:Twitter:AppId"];
                    options.ConsumerSecret = configuration["Authentication:Twitter:AppSecret"];
                });
            return services;
        }
    }
}