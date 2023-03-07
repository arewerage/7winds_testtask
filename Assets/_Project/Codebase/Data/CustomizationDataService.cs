using UnityEngine;
using Zenject;

namespace _Project.Codebase.Data
{
    public class CustomizationDataService : IInitializable
    {
        private const string CustomizationKey = "Customization";

        private readonly CustomizationData _customizationData;

        public CustomizationDataService(CustomizationData customizationData)
        {
            _customizationData = customizationData;
        }

        public void Initialize()
        {
            Load();
        }

        private void Load()
        {
            Data data = PlayerPrefs.GetString(CustomizationKey).ToDeserialized<Data>();

            if (data == null)
                return;

            _customizationData.Name = data.Name;

            _customizationData.FormData.AttributeId = data.FormId;
            _customizationData.LogoData.AttributeId = data.LogoId;

            _customizationData.FormData.FirstLayerColor.Value = data.FormFirstColor;
            _customizationData.FormData.SecondLayerColor.Value = data.FormSecondColor;
            _customizationData.FormData.ThirdLayerColor.Value = data.FormThirdColor;

            _customizationData.LogoData.FirstLayerColor.Value = data.LogoFirstColor;
            _customizationData.LogoData.SecondLayerColor.Value = data.LogoSecondColor;
            _customizationData.LogoData.ThirdLayerColor.Value = data.LogoThirdColor;
        }

        public void Save()
        {
            Data data = new()
            {
                Name = _customizationData.Name,

                FormId = _customizationData.FormData.AttributeId,
                FormFirstColor = _customizationData.FormData.FirstLayerColor.Value,
                FormSecondColor = _customizationData.FormData.SecondLayerColor.Value,
                FormThirdColor = _customizationData.FormData.ThirdLayerColor.Value,

                LogoId = _customizationData.LogoData.AttributeId,
                LogoFirstColor = _customizationData.LogoData.FirstLayerColor.Value,
                LogoSecondColor = _customizationData.LogoData.SecondLayerColor.Value,
                LogoThirdColor = _customizationData.LogoData.ThirdLayerColor.Value
            };

            PlayerPrefs.SetString(CustomizationKey, data.ToJson());
        }

        private class Data
        {
            public string Name;

            public int FormId;
            public Color FormFirstColor;
            public Color FormSecondColor;
            public Color FormThirdColor;

            public int LogoId;
            public Color LogoFirstColor;
            public Color LogoSecondColor;
            public Color LogoThirdColor;
        }
    }
}