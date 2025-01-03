using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using SistemaMedico.Data;
using SistemaMedico.Repositories.Interfaces;
using SistemaMedico.Mappings;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using SistemaMedico.Repositories;
using SistemaMedico.DTOs;
using Uol.PagSeguro.Resources;

namespace SistemaMedico
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var services = builder.Services;
            var configuration = builder.Configuration;

            services.AddControllers();

            services.AddMvc().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
            });

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddEntityFrameworkSqlServer()
                .AddDbContext<SistemaMedicoDBContex>(
                    options => options.UseSqlServer(configuration.GetConnectionString("DataBase"))
                );
            /*
            // Descomente para rodar as Seeders
            using (var serviceScope = services.BuildServiceProvider().CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<SistemaMedicoDBContex>();
                DbSeeder.Seed(dbContext);
            }
            */

            services.AddScoped<IDoutorRepository, DoutorRepository>();
            services.AddScoped<IEspecialidadeRepository, EspecialidadeRepository>();
            services.AddScoped<IPacienteRepository, PacienteRepository>();
            services.AddScoped<ITratamentoRepository, TratamentoRepository>();
            services.AddScoped<IEtapaRepository, EtapaRepository>();
            services.AddScoped<ITratamentoPacienteRepository, TratamentoPacienteRepository>();
            services.AddScoped<IAdminRepository, AdminRepository>();
            services.AddScoped<IPagamentoRepository, PagamentoRepository>();
            services.Configure<EmailSettingsDTO>(opts => configuration.GetSection("MailSettings").Bind(opts));
            services.Configure<PagSeguroSettingsDTO>(opts => configuration.GetSection("PagSeguroSettings").Bind(opts));

            services.AddAutoMapper(typeof(EnitiesDTO));

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
            })
            .AddCookie()
            .AddGoogle(options =>
            {
                options.ClientId = configuration["Authentication:Google:ClientId"];
                options.ClientSecret = configuration["Authentication:Google:ClientSecret"];
            });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors(options =>
            {
                options.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            });

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
