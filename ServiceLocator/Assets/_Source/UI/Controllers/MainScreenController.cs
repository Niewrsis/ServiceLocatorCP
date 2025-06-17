using UI.View;

namespace UI.Controllers
{
    public class MainScreenController : IUIState
    {
        private MainScreenView _mainScreenView;
        private UISwitcher _uiSwitcher;

        public MainScreenController(MainScreenView mainScreenView, UISwitcher uiSwitcher)
        {
            _mainScreenView = mainScreenView;
            _uiSwitcher = uiSwitcher;
        }

        public void Enter()
        {
            _mainScreenView.OnOpenButtonClicked += HandleOpenButtonClicked;
        }

        public void Exit()
        {
            _mainScreenView.OnOpenButtonClicked -= HandleOpenButtonClicked;
        }

        private void HandleOpenButtonClicked()
        {
            _uiSwitcher.SwitchState(UIState.Panel);
        }
    }
}