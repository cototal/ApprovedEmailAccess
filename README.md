# Approved Email Access

This is a simple library that allows you to specify a list of Emails from an exteranl service provider that are authorized to access protected pages.

## Configure Services

First, set up an external authentication provider, like Google:

```cs
public void ConfigureServices(IServiceCollection services)
{
    // ...
    services.AddAuthentication(options =>
    {
        options.DefaultScheme = "Cookies";
        options.DefaultChallengeScheme = "Google";
    })
        .AddCookie("Cookies")
        .AddGoogle("Google", options =>
        {
            options.ClientId = Configuration.GetValue<string>("GoogleAuth:ClientId");
            options.ClientSecret = Configuration.GetValue<string>("GoogleAuth:ClientSecret");
        });
    // ...
}
```

Then register an `IVerifyAdminEmailOptions` service, using either the provided `VerifyAdminEmailOptions` class or your own implementation.

```cs
services.AddSingleton<IVerifyAdminEmailOptions>(srv => new VerifyAdminEmailOptions(Configuration.GetValue<string>("AdminEmails")));
```

## Configure

Before the routes you want to protect, invoke the `VerifyAdminEmail` middleware with `UseMiddleware`

```cs
// ...
app.UseMiddleware<VerifyAdminEmail>();

// app.UseStaticFiles(), app.UseMvcWithDefaultRoute(), etc...
```
