namespace FirstMvcApp.ConfigurationProviders
{
    public class TxtConfigurationSource : IConfigurationSource
    {
        public string FilePath { get; set; }

        public TxtConfigurationSource(string filePath)
        {
            FilePath = filePath;
        }
        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            string fp = builder.GetFileProvider().GetFileInfo(FilePath).PhysicalPath;
            return new TxtConfigurationProvider(fp);
        }
    }
}
