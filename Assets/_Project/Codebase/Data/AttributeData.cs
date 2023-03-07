using _Project.Codebase.Configs;
using _Project.Codebase.UI.Views;
using UniRx;
using UnityEngine;

namespace _Project.Codebase.Data
{
    public class AttributeData
    {
        public int AttributeId;
        public readonly ReactiveProperty<Color> FirstLayerColor = new();
        public readonly ReactiveProperty<Color> SecondLayerColor = new();
        public readonly ReactiveProperty<Color> ThirdLayerColor = new();

        public AttributeData(ColorsConfig colorsConfig)
        {
            AttributeId = 0;
            FirstLayerColor.Value = colorsConfig.Colors[0];
            SecondLayerColor.Value = colorsConfig.Colors[1];
            ThirdLayerColor.Value = colorsConfig.Colors[2];
        }

        public void UpdateColors(LayerType layer, Color color)
        {
            switch (layer)
            {
                case LayerType.First:
                    FirstLayerColor.Value = color;
                    
                    break;
                case LayerType.Second:
                    SecondLayerColor.Value = color;
                    
                    break;
                case LayerType.Third:
                    ThirdLayerColor.Value = color;
                    
                    break;
            }
        }
    }
}
