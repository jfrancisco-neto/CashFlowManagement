using Microsoft.Extensions.Options;

namespace Account.Extensions;

public static class OptionsExtensions
{
    public static WebApplicationBuilder MapOption<T>(this WebApplicationBuilder builder, string section) where T : class
    {
        builder.Services.AddSingleton<T>(sp => sp.GetRequiredService<IOptions<T>>().Value);
        builder.Services.Configure<T>(builder.Configuration.GetSection(section));

        return builder;
    }
}
