using DataContext;
using Domain.Commands.Bloger;
using Domain.Handlers.Account;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Service.Mail;
using Service.Push;
using Service.Security;
using Service.Video;
using Service.YouTube;
using System.Reflection;
using Common.Authorization;
using Service.Twitch;
using viTouch.BackgroundServices;

namespace viTouch
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("Default");

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(option =>
                {
                    option.RequireHttpsMetadata = false; // ssl
                    option.TokenValidationParameters =
                        new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidIssuer = AuthenticationOptions.ISSUER,
                            ValidateAudience = true,
                            ValidAudience = AuthenticationOptions.AUDIENCE,
                            ValidateLifetime = true,
                            IssuerSigningKey = AuthenticationOptions.GetSymmetricSecurityKey(),
                            ValidateIssuerSigningKey = true
                        };
                });

            services.AddHttpClient();
            services.AddHttpClient<PushBotsService>();
            services.AddHttpClient<YouTubeService>();
            services.AddHttpClient<ITwitchService, TwitchService>();
            services.AddSingleton<ITwitchTokenProvider, TwitchTokenProvider>();
            services.AddSingleton<TwitchTokenProviderBackground>();
            services.AddHostedService<TwitchTokenProviderBackground>();

            services.AddEntityFrameworkNpgsql()
                .AddDbContext<ApplicationDbContext>(options => options
                    .UseNpgsql(connectionString, b=>b.MigrationsAssembly("vitouch")), ServiceLifetime.Transient);

            services.AddTransient<IMd5Hash, Md5Hash>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IVideoService, VideoService>();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo {Title = "viTouch API", Version = "V1"});
                options.OperationFilter<SwaggerAddHeaderParameter>();
            });

            services.AddControllers();
            services.AddMediatR(typeof(GetBlogerCommand).GetTypeInfo().Assembly);
            services.AddLogging();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            //Это пайплайн запроса, в нем крайне важен порядок вызова методов.
            //Если неправильно его указать, можно получить непредвиденный результат выполнения запроса 

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage()
                    .UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "viTouch API V1"))
                    .UseSwagger();
            }

            app.UseRouting()
                // Указываем, что приложение может обрабатывать запросы от приложений по любым адресам
                .UseCors(builder => builder.SetIsOriginAllowed(s => true).AllowAnyHeader().AllowAnyMethod().AllowCredentials())
                //проверка токена приложения
                // .UseToken()
                //проверка логина/пароля
                .UseAuthentication()
                //проверка прав вользователя
                .UseAuthorization()
                .UseStaticFiles()
                //ендпоинты контроллеров
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllerRoute(
                        name: "Default",
                        pattern: "{controller}/{action}"
                    );
                });
        }
    }
}
