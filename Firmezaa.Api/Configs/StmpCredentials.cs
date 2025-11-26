using DotNetEnv;

namespace Firmezaa.Api.Configs;

public record SmtpCredentials(
    string Host,
    int Port,
    string User,
    string Password,
    string From
);

public static class AppSettings
{
    public static readonly SmtpCredentials Smtp = new(
        Host: Env.GetString("SMTP_HOST"),
        Port: Env.GetInt("SMTP_PORT"),
        User: Env.GetString("SMTP_USER"),
        Password: Env.GetString("SMTP_PASSWORD"),
        From: Env.GetString("SMTP_FROM")
    );
}