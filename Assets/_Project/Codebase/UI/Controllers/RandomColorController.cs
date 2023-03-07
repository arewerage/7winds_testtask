using System;
using System.Linq;
using _Project.Codebase.Configs;
using _Project.Codebase.Data;
using _Project.Codebase.UI.Views;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Project.Codebase.UI.Controllers
{
    public class RandomColorController : IInitializable, IDisposable
    {
        private readonly LayerToggleController _layerToggleController;
        private readonly ReactiveProperty<ScreenType> _currentScreen;
        private readonly CustomizationData _customizationData;
        private readonly ColorsConfig _colorsConfig;
        private readonly Button _randomColorButton;
        private readonly CompositeDisposable _disposable = new();

        public RandomColorController(
            LayerToggleController layerToggleController,
            ReactiveProperty<ScreenType> currentScreen,
            CustomizationData customizationData,
            ColorsConfig colorsConfig,
            Button randomColorButton)
        {
            _layerToggleController = layerToggleController;
            _currentScreen = currentScreen;
            _customizationData = customizationData;
            _colorsConfig = colorsConfig;
            _randomColorButton = randomColorButton;
        }

        public void Initialize()
        {
            _randomColorButton
                .OnClickAsObservable()
                .Subscribe(_ =>
                {
                    Color[] colors = _colorsConfig.GetRandomColors(3).ToArray();

                    switch (_currentScreen.Value)
                    {
                        case ScreenType.Form:
                            _customizationData.FormData.FirstLayerColor.Value = colors[0];
                            _customizationData.FormData.SecondLayerColor.Value = colors[1];
                            _customizationData.FormData.ThirdLayerColor.Value = colors[2];
                            break;
                        case ScreenType.Logo:
                            _customizationData.LogoData.FirstLayerColor.Value = colors[0];
                            _customizationData.LogoData.SecondLayerColor.Value = colors[1];
                            _customizationData.LogoData.ThirdLayerColor.Value = colors[2];
                            break;
                    }
                    
                    _layerToggleController.ChangeToggleColors(_currentScreen.Value);
                })
                .AddTo(_disposable);
        }

        public void Dispose() =>
            _disposable?.Dispose();
    }
}
