using Zenject;
using UI.View;

namespace UI.Controllers
{
    public class MainScreenController : IUIState
    {
        private readonly MainScreenView _view;
        private readonly UISwitcher _uiSwitcher;

        [Inject]
        public MainScreenController(
            MainScreenView view,
            UISwitcher uiSwitcher)
        {
            _view = view;
            _uiSwitcher = uiSwitcher;
        }

        public void Enter()
        {
            _view.OnOpenButtonClicked += HandleOpenButtonClicked;
        }

        public void Exit()
        {
            _view.OnOpenButtonClicked -= HandleOpenButtonClicked;
        }

        private void HandleOpenButtonClicked()
        {
            _uiSwitcher.SwitchState(UIState.Panel);
        }
    }
}