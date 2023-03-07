using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Codebase.Data;
using _Project.Codebase.UI.Views;
using _Project.Codebase.UI.Views.Buttons;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Project.Codebase.UI.Controllers
{
    public class LayerToggleController : IInitializable, IDisposable
    {
        private readonly CustomizationData _customizationData;
        private readonly ReactiveProperty<ScreenType> _currentScreen;
        private readonly ColorSelectionController _colorSelectionController;
        private readonly ToggleGroup _layerToggleGroup;
        private readonly LayerToggle _layerTogglePrefab;
        private readonly CompositeDisposable _disposable = new();
        private readonly List<LayerToggle> _layerToggles = new(3);

        public LayerToggleController(
            CustomizationData customizationData,
            ReactiveProperty<ScreenType> currentScreen,
            ColorSelectionController colorSelectionController,
            ToggleGroup layerToggleGroup,
            LayerToggle layerTogglePrefab)
        {
            _customizationData = customizationData;
            _currentScreen = currentScreen;
            _colorSelectionController = colorSelectionController;
            _layerToggleGroup = layerToggleGroup;
            _layerTogglePrefab = layerTogglePrefab;
        }

        public void Initialize()
        {
            for (int index = 0; index < 3; index++)
            {
                LayerToggle layerToggle = UnityEngine.Object.Instantiate(_layerTogglePrefab, _layerToggleGroup.transform);
                layerToggle.LayerType = (LayerType)index;
                layerToggle.Toggle.group = _layerToggleGroup;
                
                _layerToggles.Add(layerToggle);
            }

            _colorSelectionController.ActiveColor
                .ObserveEveryValueChanged(color => color.Value)
                .Subscribe(OnActiveColorChanged)
                .AddTo(_disposable);

            _currentScreen
                .ObserveEveryValueChanged(screen => screen.Value)
                .Subscribe(ChangeToggleColors)
                .AddTo(_disposable);
        }

        public void ChangeToggleColors(ScreenType screenType)
        {
            switch (screenType)
            {
                case ScreenType.Form:
                    _layerToggles[0].Color = _customizationData.FormData.FirstLayerColor.Value;
                    _layerToggles[1].Color = _customizationData.FormData.SecondLayerColor.Value;
                    _layerToggles[2].Color = _customizationData.FormData.ThirdLayerColor.Value;
                    break;
                case ScreenType.Logo:
                    _layerToggles[0].Color = _customizationData.LogoData.FirstLayerColor.Value;
                    _layerToggles[1].Color = _customizationData.LogoData.SecondLayerColor.Value;
                    _layerToggles[2].Color = _customizationData.LogoData.ThirdLayerColor.Value;
                    break;
            }
        }

        public void Dispose()
        {
            _disposable?.Dispose();
            _layerToggles.Clear();
        }

        private void OnActiveColorChanged(Color color)
        {
            foreach (LayerToggle layerToggle in _layerToggles.Where(layerToggle => layerToggle.Toggle.isOn))
            {
                layerToggle.Color = color;

                switch (_currentScreen.Value)
                {
                    case ScreenType.Form:
                        _customizationData.FormData.UpdateColors(layerToggle.LayerType, color);
                        break;
                    case ScreenType.Logo:
                        _customizationData.LogoData.UpdateColors(layerToggle.LayerType, color);
                        break;
                }
            }
        }
    }
}
