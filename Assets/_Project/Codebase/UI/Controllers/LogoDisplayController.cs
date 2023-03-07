using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Codebase.Configs;
using _Project.Codebase.Data;
using _Project.Codebase.UI.Views;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Attribute = _Project.Codebase.UI.Views.Attribute.Attribute;
using Object = UnityEngine.Object;

namespace _Project.Codebase.UI.Controllers
{
    public class LogoDisplayController : AttributeDisplayController, IInitializable, IDisposable
    {
        private readonly ReactiveProperty<ScreenType> _currentScreen;
        private readonly CustomizationData _customizationData;
        private readonly AttributeConfig _logoConfig;
        private readonly Transform _logoPreviewGroup;
        private readonly Transform _formAndLogoPreview;
        private readonly List<Button> _attributeButtons;
        private readonly List<Attribute> _logos = new();
        private readonly CompositeDisposable _disposable = new();
        
        private int _currentLogoIndex;
        
        public LogoDisplayController(
            ReactiveProperty<ScreenType> currentScreen,
            CustomizationData customizationData,
            AttributeConfig logoConfig,
            Transform logoPreviewGroup,
            [Inject(Id = "FormAndLogoPreview")] Transform formAndLogoPreview,
            List<Button> attributeButtons) : base(currentScreen)
        {
            _currentScreen = currentScreen;
            _customizationData = customizationData;
            _logoConfig = logoConfig;
            _logoPreviewGroup = logoPreviewGroup;
            _formAndLogoPreview = formAndLogoPreview;
            _attributeButtons = attributeButtons;
        }

        public void Initialize()
        {
            _currentLogoIndex = _customizationData.LogoData.AttributeId;

            foreach ((int key, Attribute logoPrefab) in _logoConfig.Attributes)
            {
                Attribute logo = Object.Instantiate(logoPrefab, _logoPreviewGroup);
                logo.Id = key;
                logo.SetY(400f);
                logo.SetAlpha(key == _currentLogoIndex ? 1f : 0f);
                
                _logos.Add(logo);

                _customizationData.LogoData.FirstLayerColor
                    .ObserveEveryValueChanged(color => color.Value)
                    .Subscribe(logo.SetFirstColor)
                    .AddTo(_disposable);
                
                _customizationData.LogoData.SecondLayerColor
                    .ObserveEveryValueChanged(color => color.Value)
                    .Subscribe(logo.SetSecondColor)
                    .AddTo(_disposable);
                
                _customizationData.LogoData.ThirdLayerColor
                    .ObserveEveryValueChanged(color => color.Value)
                    .Subscribe(logo.SetThirdColor)
                    .AddTo(_disposable);
            }

            _attributeButtons[0]
                .OnClickAsObservable()
                .Subscribe(_ => ShowPrevious(ScreenType.Logo, _logos, ref _currentLogoIndex))
                .AddTo(_disposable);
            
            _attributeButtons[1]
                .OnClickAsObservable()
                .Subscribe(_ => ShowNext(ScreenType.Logo, _logos, ref _currentLogoIndex, _logoConfig.Attributes))
                .AddTo(_disposable);
            
            _currentScreen
                .ObserveEveryValueChanged(screen => screen.Value)
                .Subscribe(OnScreenChanged)
                .AddTo(_disposable);
        }
        
        public void Dispose()
        {
            _disposable?.Dispose();
            _logos.Clear();
        }

        protected override void ShowNext(
            ScreenType workingScreen,
            List<Attribute> attributes,
            ref int currentAttributeIndex,
            Dictionary<int, Attribute> attributesConfig)
        {
            base.ShowNext(workingScreen, attributes, ref currentAttributeIndex, attributesConfig);
            _customizationData.LogoData.AttributeId = currentAttributeIndex;
        }

        protected override void ShowPrevious(
            ScreenType workingScreen,
            List<Attribute> attributes,
            ref int currentAttributeIndex)
        {
            base.ShowPrevious(workingScreen, attributes, ref currentAttributeIndex);
            _customizationData.LogoData.AttributeId = currentAttributeIndex;
        }
        
        private void OnScreenChanged(ScreenType currentScreen)
        {
            if (currentScreen != ScreenType.Name)
                return;
            
            Attribute _formPreviewObject = Object.Instantiate(_logos.First(form => form.Id == _currentLogoIndex), _formAndLogoPreview);
            _formPreviewObject.SetAlpha(0f);
            _formPreviewObject.FadeOut();
        }
    }
}