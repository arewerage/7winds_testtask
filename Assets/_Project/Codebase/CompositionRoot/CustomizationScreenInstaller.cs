using System.Collections.Generic;
using _Project.Codebase.UI.Controllers;
using _Project.Codebase.UI.Views;
using _Project.Codebase.UI.Views.Buttons;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Screen = _Project.Codebase.UI.Views.Screens.Screen;

namespace _Project.Codebase.CompositionRoot
{
    public class CustomizationScreenInstaller : MonoInstaller
    {
        public ReactiveProperty<ScreenType> CurrentScreen = new(ScreenType.Form);
        
        [Header("Color Palette")]
        [SerializeField] private Transform _colorPaletteGroup;
        [SerializeField] private ColorButton _colorButtonPrefab;

        [Header("Screens")]
        [SerializeField] private Screen _attributeScreenGroup;
        [SerializeField] private List<Screen> _screens;
        [SerializeField] private Button _nextScreenButton;

        [Header("Team Name Input")]
        [SerializeField] private TMP_InputField _teamNameInput;

        [Header("Random Colors")]
        [SerializeField] private Button _randomColorButton;

        [Header("Layer Toggles")]
        [SerializeField] private ToggleGroup _layerToggleGroup;
        [SerializeField] private LayerToggle _layerTogglePrefab;

        [Header("Attribute Selection")]
        [SerializeField] private Transform _formPreviewGroup;
        [SerializeField] private Transform _logoPreviewGroup;
        [SerializeField] private List<Button> _attributeButtons = new(2);

        [Header("Preview")]
        [SerializeField] private Transform _formPreview;
        [SerializeField] private Transform _formAndLogoPreview;

        public override void InstallBindings()
        {
            Container.BindInstance(CurrentScreen).AsSingle();
            
            BindColorPaletteControllers();
            BindScreenControllers();
            BindTeamNameInputControllers();
            BindRandomColorControllers();
            BindAttributeLayerSelectionControllers();
            BindAttributeControllers();
        }

        private void BindColorPaletteControllers()
        {
            Container.BindInterfacesAndSelfTo<ColorSelectionController>().AsSingle();
            Container.BindInstance(_colorPaletteGroup).WhenInjectedInto<ColorSelectionController>();
            Container.BindInstance(_colorButtonPrefab).WhenInjectedInto<ColorSelectionController>();
        }

        private void BindScreenControllers()
        {
            Container.BindInterfacesAndSelfTo<ScreenPlacementController>().AsSingle();
            Container.BindInstance(_attributeScreenGroup).WhenInjectedInto<ScreenPlacementController>();
            Container.BindInstance(_screens).WhenInjectedInto<ScreenPlacementController>();

            Container.BindInterfacesAndSelfTo<ScreenChangingController>().AsSingle();
            Container.BindInstance(_nextScreenButton).WhenInjectedInto(typeof(ScreenChangingController), typeof(TeamNameInputController));
        }

        private void BindTeamNameInputControllers()
        {
            Container.BindInterfacesAndSelfTo<TeamNameInputController>().AsSingle();
            Container.BindInstance(_teamNameInput).WhenInjectedInto<TeamNameInputController>();
        }

        private void BindRandomColorControllers()
        {
            Container.BindInterfacesAndSelfTo<RandomColorController>().AsSingle();
            Container.BindInstance(_randomColorButton).WhenInjectedInto<RandomColorController>();
        }
        
        private void BindAttributeLayerSelectionControllers()
        {
            Container.BindInterfacesAndSelfTo<LayerToggleController>().AsSingle();
            Container.BindInstance(_layerToggleGroup).WhenInjectedInto<LayerToggleController>();
            Container.BindInstance(_layerTogglePrefab).WhenInjectedInto<LayerToggleController>();
        }

        private void BindAttributeControllers()
        {
            Container.BindInterfacesAndSelfTo<FormDisplayController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<LogoDisplayController>().AsSingle().NonLazy();
            
            Container.BindInstance(_formPreviewGroup).WithId("FormPreviewGroup").WhenInjectedInto<FormDisplayController>();
            Container.BindInstance(_logoPreviewGroup).WhenInjectedInto<LogoDisplayController>();
            Container.BindInstance(_attributeButtons).WhenInjectedInto(typeof(FormDisplayController), typeof(LogoDisplayController));
            
            Container.BindInstance(_formPreview).WithId("FormPreview").WhenInjectedInto<FormDisplayController>();
            Container.BindInstance(_formAndLogoPreview).WithId("FormAndLogoPreview")
                .WhenInjectedInto(typeof(FormDisplayController), typeof(LogoDisplayController));
        }
    }
}
