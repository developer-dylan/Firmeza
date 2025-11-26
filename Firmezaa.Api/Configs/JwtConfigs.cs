namespace Firmezaa.Api.Configs;

public static class JwtConfigs
{
    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services)
    {
        var jwtKey = Environment.GetEnvironmentVariable("JWT_KEY");
        var jwtIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER");
        var jwtAudience = Environment.GetEnvironmentVariable("JWT_AUDIENCE");

        if (string.IsNullOrEmpty(jwtKey))
            throw new InvalidOperationException("JWT_KEY no está configurado en las variables de entorno.");

        if (string.IsNullOrEmpty(jwtIssuer))
            throw new InvalidOperationException("JWT_ISSUER no está configurado en las variables de entorno.");

        if (string.IsNullOrEmpty(jwtAudience))
            throw new InvalidOperationException("JWT_AUDIENCE no está configurado en las variables de entorno.");

        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtIssuer,
                    ValidAudience = jwtAudience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
                };
            });

        return services;
    }
}