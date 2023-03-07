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
    public class FormDisplayController : AttributeDisplayController, IInitializable, IDisposable
    {
        private readonly ReactiveProperty<ScreenType> _currentScreen;
        private readonly CustomizationData _customizationData;
        private readonly AttributeConfig _formConfig;
        private readonly Transform _formPreviewGroup;
        private readonly Transform _formPreview;
        private readonly Transform _formAndLogoPreview;
        private readonly List<Button> _attributeButtons;
        private readonly List<Attribute> _forms = new();
        private readonly CompositeDisposable _disposable = new();
        
        private int _currentFormIndex;
        private Attribute _formPreviewObject;
        
        public FormDisplayController(
            ReactiveProperty<ScreenType> currentScreen,
            CustomizationData customizationData,
            AttributeConfig formConfig,
            [Inject(Id = "FormPreviewGroup")] Transform formPreviewGroup,
            [Inject(Id = "FormPreview")] Transform formPreview,
            [Inject(Id = "FormAndLogoPreview")] Transform formAndLogoPreview,
            List<Button> attributeButtons) : base(currentScreen)
        {
            _currentScreen = currentScreen;
            _customizationData = customizationData;
            _formConfig = formConfig;
            _formPreviewGroup = formPreviewGroup;
            _formPreview = formPreview;
            _formAndLogoPreview = formAndLogoPreview;
            _attributeButtons = attributeButtons;
        }

        public void Initialize()
        {
            _currentFormIndex = _customizationData.FormData.AttributeId;

            foreach ((int key, Attribute formPrefab) in _formConfig.Attributes)
            {
                Attribute form = Object.Instantiate(formPrefab, _formPreviewGroup);
                form.Id = key;
                form.SetY(400f);
                form.SetAlpha(key == _currentFormIndex ? 1f : 0f);
                
                _forms.Add(form);

                _customizationData.FormData.FirstLayerColor
                    .ObserveEveryValueChanged(color => color.Value)
                    .Subscribe(form.SetFirstColor)
                    .AddTo(_disposable);
                
                _customizationData.FormData.SecondLayerColor
                    .ObserveEveryValueChanged(color => color.Value)
                    .Subscribe(form.SetSecondColor)
                    .AddTo(_disposable);
                
                _customizationData.FormData.ThirdLayerColor
                    .ObserveEveryValueChanged(color => color.Value)
                    .Subscribe(form.SetThirdColor)
                    .AddTo(_disposable);
            }

            _attributeButtons[0]
                .OnClickAsObservable()
                .Subscribe(_ => ShowPrevious(ScreenType.Form, _forms, ref _currentFormIndex))
                .AddTo(_disposable);
            
            _attributeButtons[1]
                .OnClickAsObservable()
                .Subscribe(_ => ShowNext(ScreenType.Form, _forms, ref _currentFormIndex, _formConfig.Attributes))
                .AddTo(_disposable);

            _currentScreen
                .ObserveEveryValueChanged(screen => screen.Value)
                .Subscribe(OnScreenChanged)
                .AddTo(_disposable);
        }
        
        public void Dispose()
        {
            _disposable?.Dispose();
            _forms.Clear();
        }

        protected override void ShowNext(
            ScreenType workingScreen,
            List<Attribute> attributes,
            ref int currentAttributeIndex,
            Dictionary<int, Attribute> attributesConfig)
        {
            base.ShowNext(workingScreen, attributes, ref currentAttributeIndex, attributesConfig);
            _customizationData.FormData.AttributeId = currentAttributeIndex;
        }

        protected override void ShowPrevious(
            ScreenType workingScreen,
            List<Attribute> attributes,
            ref int currentAttributeIndex)
        {
            base.ShowPrevious(workingScreen, attributes, ref currentAttributeIndex);
            _customizationData.FormData.AttributeId = currentAttributeIndex;
        }

        private void OnScreenChanged(ScreenType currentScreen)
        {
            if (currentScreen == ScreenType.Form)
                return;
                    
            switch (currentScreen)
            {
                case ScreenType.Logo:
                    
                    _formPreviewObject = Object.Instantiate(_forms.First(form => form.Id == _currentFormIndex), _formPreview);
                    _formPreviewObject.SetAlpha(0f);
                    _formPreviewObject.FadeOut();
                    
                    break;
                case ScreenType.Name:
                    
                    _formPreviewObject?.FadeIn();
                    Attribute formWithLogoPreviewObject = Object.Instantiate(_formPreviewObject, _formAndLogoPreview);
                    formWithLogoPreviewObject.SetAlpha(0f);
                    formWithLogoPreviewObject.FadeOut();
                    
                    break;
            }
        }
    }
}