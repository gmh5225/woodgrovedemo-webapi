using Microsoft.Identity.Web;
using Microsoft.Extensions.Logging.AzureAppServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.IdentityModel.Tokens.Jwt;

var builder = WebApplication.CreateBuilder(args);

// Add Azure stream log service
builder.Logging.AddAzureWebAppDiagnostics();
builder.Services.Configure<AzureFileLoggerOptions>(options =>
{
    options.FileName = "azure-diagnostics-";
    options.FileSizeLimit = 50 * 1024;
    options.RetainedFileCountLimit = 5;
});
builder.Logging.AddFilter((provider, category, logLevel) =>
{
    return provider.ToLower().Contains("woodgroveapi");
});

// Disable the default claims mapping.
JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Verifies that the caller of the Web API is always the CIAM STS.
string policyName = "VerifyCallerIsCiamSts";
builder.Services.AddAuthorization(options => {
    options.AddPolicy(policyName, builder =>
    {
        // See https://learn.microsoft.com/azure/active-directory/develop/custom-extension-overview#protect-your-rest-api
        builder.RequireClaim("azp", "99045fe1-7639-4a75-9d4a-577b6ca3810f");
    });
    options.DefaultPolicy = options.GetPolicy(policyName)!;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
