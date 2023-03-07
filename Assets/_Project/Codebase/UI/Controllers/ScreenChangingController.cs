using System;
using _Project.Codebase.Configs;
using _Project.Codebase.Data;
using _Project.Codebase.UI.Views;
using UniRx;
using UnityEngine.UI;
using Zenject;

namespace _Project.Codebase.UI.Controllers
{
    public class ScreenChangingController : IInitializable, IDisposable
    {
        private const float ChangingDuration = 0.5f;

        private readonly CustomizationDataService _customizationDataService;
        private readonly ReactiveProperty<ScreenType> _currentScreen;
        private readonly ScreenPlacementController _screenPlacementController;
        private readonly Button _nextScreenButton;
        private readonly CompositeDisposable _disposable = new();

        public ScreenChangingController(
            CustomizationDataService customizationDataService,
            ReactiveProperty<ScreenType> currentScreen,
            ScreenPlacementController screenPlacementController,
            Button nextScreenButton)
        {
            _customizationDataService = customizationDataService;
            _currentScreen = currentScreen;
            _screenPlacementController = screenPlacementController;
            _nextScreenButton = nextScreenButton;
        }

        public void Initialize()
        {
            _nextScreenButton
                .OnClickAsObservable()
                .ThrottleFirst(TimeSpan.FromSeconds(ChangingDuration))
                .Subscribe(_ => ChangeScreen(_currentScreen.Value, ChangingDuration))
                .AddTo(_disposable);
            
            _screenPlacementController.GoToNextScreen(_currentScreen.Value, 0f);
        }

        public void Dispose() =>
            _disposable?.Dispose();

        private void ChangeScreen(ScreenType currentScreen, float duration)
        {
            _customizationDataService.Save();
            
            if (currentScreen.TryGetNext(out ScreenType nextScreen) == false)
                return;
            
            _screenPlacementController.GoToNextScreen(nextScreen, duration);
            _currentScreen.Value = nextScreen;
        }
    }

    public class AttributePreviewController : IInitializable, IDisposable
    {
        private readonly ReactiveProperty<ScreenType> _currentScreen;
        private readonly CustomizationData _customizationData;
        private readonly AttributeConfig _formConfig;
        private readonly AttributeConfig _logoConfig;
        private readonly CompositeDisposable _disposable = new();

        public AttributePreviewController(
            ReactiveProperty<ScreenType> currentScreen,
            CustomizationData customizationData,
            [Inject(Id = "Form")] AttributeConfig formConfig,
            [Inject(Id = "Form")] AttributeConfig logoConfig)
        {
            _currentScreen = currentScreen;
            _customizationData = customizationData;
            _formConfig = formConfig;
            _logoConfig = logoConfig;
        }

        public void Initialize()
        {
            _currentScreen
                .ObserveEveryValueChanged(screen => screen.Value)
                .Subscribe(OnScreenChanged)
                .AddTo(_disposable);
        }

        public void Dispose()
        {
            _disposable?.Dispose();
        }

        private void OnScreenChanged(ScreenType currentScreen)
        {
            switch (currentScreen)
            {
                case ScreenType.Logo:
                    break;
                case ScreenType.Name:
                    break;
            }
        }
    }
}
