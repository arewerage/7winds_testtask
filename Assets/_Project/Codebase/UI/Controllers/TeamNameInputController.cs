using System;
using _Project.Codebase.Configs;
using _Project.Codebase.Data;
using _Project.Codebase.UI.Views;
using ModestTree;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Project.Codebase.UI.Controllers
{
    public class TeamNameInputController : IInitializable, IDisposable
    {
        private const int MaxNameLength = 10;

        private readonly CustomizationData _customizationData;
        private readonly ReactiveProperty<ScreenType> _currentScreen;
        private readonly TMP_InputField _teamNameInput;
        private readonly ColorsConfig _colorsConfig;
        private readonly Button _nextScreenButton;
        private readonly CompositeDisposable _disposable = new();

        public TeamNameInputController(
            CustomizationData customizationData,
            ReactiveProperty<ScreenType> currentScreen,
            TMP_InputField teamNameInput,
            ColorsConfig colorsConfig,
            Button nextScreenButton)
        {
            _customizationData = customizationData;
            _currentScreen = currentScreen;
            _teamNameInput = teamNameInput;
            _colorsConfig = colorsConfig;
            _nextScreenButton = nextScreenButton;
        }

        public void Initialize()
        {
            _teamNameInput
                .ObserveEveryValueChanged(input => input.text)
                .Subscribe(ValidateName)
                .AddTo(_disposable);

            _teamNameInput.text = _customizationData.Name;
        }

        public void Dispose() =>
            _disposable?.Dispose();

        private void ValidateName(string name)
        {
            bool isValid = name.Length <= MaxNameLength && name.IsEmpty() == false;
            _teamNameInput.textComponent.color = isValid ? Color.white : _colorsConfig.GetErrorColor();
            _nextScreenButton.interactable = isValid;
            
            if (_currentScreen.Value == ScreenType.Name && isValid)
                _customizationData.Name = name;
        }
    }
}
