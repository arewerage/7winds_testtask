using _Project.Codebase.Configs;
using _Project.Codebase.Data;
using _Project.Codebase.UI.Controllers;
using Zenject;

namespace _Project.Codebase.CompositionRoot
{
    public class SceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ColorsConfig>().FromScriptableObjectResource("Configs/ColorsConfig").AsSingle();
            Container.Bind<AttributeConfig>().FromScriptableObjectResource("Configs/FormConfig").WhenInjectedInto<FormDisplayController>();
            Container.Bind<AttributeConfig>().FromScriptableObjectResource("Configs/LogoConfig").WhenInjectedInto<LogoDisplayController>();
            Container.Bind<CustomizationData>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<CustomizationDataService>().AsSingle().NonLazy();
        }
    }
}
