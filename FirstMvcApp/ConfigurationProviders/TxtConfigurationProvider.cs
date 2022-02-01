namespace FirstMvcApp.ConfigurationProviders;

public class TxtConfigurationProvider : ConfigurationProvider
{
    public string FilePath { get; set; }

    public TxtConfigurationProvider(string filePath)
    {
        FilePath = filePath;
    }

    public override void Load()
    {
        var data = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);

        using (var sr = new StreamReader(FilePath))
        {
            //todo read file and fill data-dictionary
        }

        Data = data;
    }
}