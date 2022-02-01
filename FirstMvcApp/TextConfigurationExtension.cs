using FirstMvcApp.ConfigurationProviders;

namespace FirstMvcApp;

public static class TextConfigurationExtensions
{
    public static IConfigurationBuilder AddTxtConfiguration(this IConfigurationBuilder builder,string path)
    {
        if (builder == null )
        {
            throw new ArgumentException(nameof(builder));
        }

        if (string.IsNullOrEmpty(path))
        {
            throw new ArgumentException(nameof(path));
        }

        var source = new TxtConfigurationSource(path);
        builder.Add(source);
        return builder;
    }
}