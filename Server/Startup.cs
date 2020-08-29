using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
using Server.Services;
using Server.Services.Dao;

namespace Server
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddGrpc();
            services.AddCors(o => o.AddPolicy("AllowAll", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .WithExposedHeaders("Grpc-Status", "Grpc-Message", "Grpc-Encoding", "Grpc-Accept-Encoding");
            }));

            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.Authority = EnvironmentUtils.GetEnvironmentVariable("JWT_AUTHORITY", "http://localhost:8080/auth/realms/LittleFighter/");
                    options.Audience = EnvironmentUtils.GetEnvironmentVariable("JWT_AUDIENCE", "account");
                    options.RequireHttpsMetadata = false;
                });

            services.AddAuthorization();

            services.AddHttpContextAccessor();

            services.AddSingleton(GetMongoDatabase());
            services.AddSingleton<PlayerDao>();
            services.AddSingleton<PlayerService>();
            services.AddSingleton<HttpContext>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseGrpcWeb(new GrpcWebOptions { DefaultEnabled = true });
            app.UseCors();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<PlayerController>().RequireCors("AllowAll");
            });
        }

        private IMongoDatabase GetMongoDatabase()
        {
            string host = EnvironmentUtils.GetEnvironmentVariable("MONGODB_HOST", "localhost");
            string port = EnvironmentUtils.GetEnvironmentVariable("MONGODB_PORT", "27017");
            string database = EnvironmentUtils.GetEnvironmentVariable("MONGODB_DATABASE", "default");
            string username = EnvironmentUtils.GetEnvironmentVariable("MONGODB_AUTHENTICATION_USERNAME", "mongo");
            string password = EnvironmentUtils.GetEnvironmentVariable("MONGODB_AUTHENTICATION_PASSWORD", "mongo");
            string authenticationDatabase = EnvironmentUtils.GetEnvironmentVariable("MONGODB_AUTHENTICATION_DATABASE", "admin");

            var settings = new MongoClientSettings
            {
                Server = new MongoServerAddress(host, int.Parse(port)),
                Credential = MongoCredential.CreateCredential(authenticationDatabase, username, password)
            };
            var mongoClient = new MongoClient(settings);

            return mongoClient.GetDatabase(database);
        }
    }
}