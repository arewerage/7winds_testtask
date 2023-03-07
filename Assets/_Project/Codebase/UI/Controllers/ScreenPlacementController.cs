using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Codebase.UI.Views;
using _Project.Codebase.UI.Views.Screens;

namespace _Project.Codebase.UI.Controllers
{
    public class ScreenPlacementController
    {
        private readonly Screen _attributeScreenGroup;
        private readonly List<Screen> _screens;

        public ScreenPlacementController(Screen attributeScreenGroup, List<Screen> screens)
        {
            _attributeScreenGroup = attributeScreenGroup;
            _screens = screens;
        }

        public void GoToNextScreen(ScreenType nextScreen, float duration)
        {
            int startingOrderIndex;

            switch (nextScreen)
            {
                case ScreenType.Form:

                    startingOrderIndex = (int)ScreenOrder.Center;

                    _attributeScreenGroup.MoveByOrder(startingOrderIndex, duration);
                    foreach (Screen screen in _screens)
                        screen.MoveByOrder(startingOrderIndex++, duration);

                    break;
                case ScreenType.Logo:

                    startingOrderIndex = (int)ScreenOrder.Left;

                    foreach (Screen screen in _screens)
                        screen.MoveByOrder(startingOrderIndex++, duration);

                    break;
                case ScreenType.Name:

                    startingOrderIndex = (int)ScreenOrder.Left;

                    _attributeScreenGroup.MoveByOrder(startingOrderIndex++, duration);
                    _screens.Last().MoveByOrder(startingOrderIndex, duration);

                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(nextScreen), nextScreen, null);
            }
        }
    }
}
