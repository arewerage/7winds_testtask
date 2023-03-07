using _Project.Codebase.Configs;

namespace _Project.Codebase.Data
{
    public class CustomizationData
    {
        public string Name;
        public readonly AttributeData FormData;
        public readonly AttributeData LogoData;

        public CustomizationData(ColorsConfig colorsConfig)
        {
            Name = "Your Name";
            FormData = new AttributeData(colorsConfig);
            LogoData = new AttributeData(colorsConfig);
        }
    }
}
