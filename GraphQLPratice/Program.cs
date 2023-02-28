using GraphQLPratice.GraphQL;
using GraphQLPratice.Filter;
using ProjectPratice.Service.Interface;
using ProjectPratice.Service.Implement;
using ProjectPratice.Repository.Interface;
using ProjectPratice.Repository.Implement;
using ProjectPratice.Common;
using DataAnnotatedModelValidations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .CreateBootstrapLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    //Initial Serilog with appsetting
    builder.Host.UseSerilog(
        (hostingContext, services, loggerConfiguration) => {
            loggerConfiguration.ReadFrom.Configuration(builder.Configuration); 
        });

    // Add services to the container.
    builder.Services.AddScoped<ICardService, CardService>();
    builder.Services.AddScoped<IUserService, UserService>();
    builder.Services.AddScoped<IUserRepository>(sp => {
        var connectString = builder.Configuration.GetSection("ConnectionStrings").Get<string>();
        return new UserRepository(connectString);
    });
    builder.Services.AddScoped<ICardRepository>(sp => {
        var connectString = builder.Configuration.GetSection("ConnectionStrings").Get<string>();
        return new CardRepository(connectString);
    });

    //JWT Authentication
    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = builder.Configuration.GetSection("JWTtokenSetting").GetValue<string>("Issuer"),
                ValidateIssuer = true,
                ValidAudience = builder.Configuration.GetSection("JWTtokenSetting").GetValue<string>("Audience"),
                ValidateAudience = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JWTtokenSetting").GetValue<string>("Key"))),
                ValidateIssuerSigningKey = true
            };
        });

    //Authorization policy
    builder.Services.AddAuthorization(options => {
        options.AddPolicy("Admin-policy", policy => { policy.RequireClaim("Role", Role.admin.ToString());});
    });

    //GraphQL injection
    builder.Services
        .AddGraphQLServer()
        .AddAuthorization()
        .AddDataAnnotationsValidator()  //need downgrade hot chocolate
        .AddQueryType<Query>()
        .AddMutationType<Mutation>();
 
    //Handle GraphQL expection
    builder.Services.AddErrorFilter<GraphQLErrorFilter>();

   
    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment()){}

    //Using log and log every request
    app.UseSerilogRequestLogging(options => {

        // Customize the message template
        options.MessageTemplate = "{RemoteIpAddress} {RequestScheme} {RequestHost} {RequestMethod} {RequestPath} responded {StatusCode} in {Elapsed:0.0000} ms";
        
        // Attach additional properties to the request completion event
        options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
        {
            diagnosticContext.Set("RequestHost", httpContext.Request.Host.Value);
            diagnosticContext.Set("RequestScheme", httpContext.Request.Scheme);
            diagnosticContext.Set("RemoteIpAddress", httpContext.Connection.RemoteIpAddress);
        };
    });

    //Using Auth
    app.UseAuthentication();
    app.UseAuthorization();

    //Using Graphql
    app.MapGraphQL();

    app.Run();

    return 0;

}catch(Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly.");
    return 1;
}
finally
{
    Log.CloseAndFlush();
}


