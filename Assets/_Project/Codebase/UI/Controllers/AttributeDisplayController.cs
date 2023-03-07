using System.Collections.Generic;
using System.Linq;
using _Project.Codebase.UI.Views;
using UniRx;
using Attribute = _Project.Codebase.UI.Views.Attribute.Attribute;

namespace _Project.Codebase.UI.Controllers
{
    public class AttributeDisplayController 
    {
        private readonly ReactiveProperty<ScreenType> _currentScreen;

        protected AttributeDisplayController(ReactiveProperty<ScreenType> currentScreen)
        {
            _currentScreen = currentScreen;
        }

        protected virtual void ShowNext(
            ScreenType workingScreen,
            List<Attribute> attributes,
            ref int currentAttributeIndex,
            Dictionary<int, Attribute> attributesConfig)
        {
            if (_currentScreen.Value != workingScreen)
                return;
            
            if (attributesConfig.Count - 1 > currentAttributeIndex)
            {
                int i = currentAttributeIndex;
                attributes.First(form => form.Id == i).Hide(Attribute.AnimateSide.Left);
                i++;
                attributes.First(form => form.Id == i).Show(Attribute.AnimateSide.Right);
                currentAttributeIndex = i;
            }
        }

        protected virtual void ShowPrevious(
            ScreenType workingScreen,
            List<Attribute> attributes,
            ref int currentAttributeIndex)
        {
            if (_currentScreen.Value != workingScreen)
                return;
            
            if (currentAttributeIndex > 0)
            {
                int i = currentAttributeIndex;
                attributes.First(form => form.Id == i).Hide(Attribute.AnimateSide.Right);
                i--;
                attributes.First(form => form.Id == i).Show(Attribute.AnimateSide.Left);
                currentAttributeIndex = i;
            }
        }
    }
}