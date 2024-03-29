using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using stationeryManagement.Code;
using stationeryManagement.Middlewares;
using System.Text;
using stationeryManagement.Service.SignalRService;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSignalR();

// builder.WebHost.ConfigureKestrel(serverOptions =>
// {
//     serverOptions.ListenAnyIP(5000);
// });
// builder.Host.ConfigureAppConfiguration((context, config) =>
// {
//     config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
// });
builder.Services.AddCors(options =>
{
    options.AddPolicy("MyPolicy", builder =>
    {
        builder.WithOrigins("http://localhost:4200","http://127.0.0.1:5500","*")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});
// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                var accessToken = context.Request.Query["access_token"];

                // If the request is for our hub...
                var path = context.HttpContext.Request.Path;
                if (!string.IsNullOrEmpty(accessToken) &&
                    (path.StartsWithSegments("/notify-hub")))
                {
                    // Read the token out of the query string
                    context.Token = accessToken;
                }
                return Task.CompletedTask;
            }
        };
    });
builder.RegisterDependencies();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseCors("MyPolicy");
app.MapHub<MessageHub>("/notify-hub");
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.MapControllers();
app.Run();