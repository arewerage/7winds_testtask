using System;
using _Project.Codebase.Configs;
using _Project.Codebase.UI.Views.Buttons;
using UniRx;
using UnityEngine;
using Zenject;

namespace _Project.Codebase.UI.Controllers
{
    public class ColorSelectionController : IInitializable, IDisposable
    {
        public ReactiveProperty<Color> ActiveColor { get; } = new(Color.white);

        private readonly ColorsConfig _colorsConfig;
        private readonly ColorButton _colorButtonPrefab;
        private readonly Transform _colorPaletteGroup;
        private readonly CompositeDisposable _disposable = new();

        public ColorSelectionController(ColorsConfig colorsConfig, ColorButton colorButtonPrefab, Transform colorPaletteGroup)
        {
            _colorsConfig = colorsConfig;
            _colorButtonPrefab = colorButtonPrefab;
            _colorPaletteGroup = colorPaletteGroup;
        }

        public void Initialize()
        {
            foreach (Color color in _colorsConfig.Colors)
            {
                ColorButton colorButton = UnityEngine.Object.Instantiate(_colorButtonPrefab, _colorPaletteGroup);
                colorButton.Color = color;
                colorButton.Button
                    .OnClickAsObservable()
                    .Subscribe(_ => ActiveColor.Value = colorButton.Color)
                    .AddTo(_disposable);
            }
        }

        public void Dispose() =>
            _disposable?.Dispose();
    }
}
