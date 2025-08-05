namespace MusicShare.Backend;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();
        var origin = "basic"; 
        builder.Services.AddCors(options =>
        {
            options.AddPolicy(name: origin,
                policy =>
                {
                    policy.WithOrigins("http://localhost:5173", "https://andyxie.cn:4000", "http://localhost:3000",
                            "https://andyxie.cn:4001")
                        .WithHeaders("Content-Type").AllowCredentials();
                });
        });
        builder.Services.AddDistributedMemoryCache();
        builder.Services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromDays(7);
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
            options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
        });
        var app = builder.Build();
        
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }
       
        app.UseCors(origin); 
        app.UseAuthorization();
        app.UseSession(); 

        app.MapControllers();

        app.Run();
    }
}