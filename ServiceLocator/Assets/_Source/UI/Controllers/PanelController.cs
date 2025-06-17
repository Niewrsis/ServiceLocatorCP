using Zenject;
using UI.View;
using Services;
using ScoreSystem;


namespace UI.Controllers
{
    public class PanelController : IUIState
    {
        private readonly PanelView _view;
        private readonly UISwitcher _uiSwitcher;
        private readonly Score _score;
        private readonly ISaver _saver;

        [Inject]
        public PanelController(
            PanelView view,
            UISwitcher uiSwitcher,
            Score score,
            ISaver saver)
        {
            _view = view;
            _uiSwitcher = uiSwitcher;
            _score = score;
            _saver = saver;
        }

        public void Enter()
        {
            _view.UpdateScore(_score.CurrentScore);
            _view.OnCloseButtonClicked += HandleCloseButtonClicked;
            _view.OnCollectButtonClicked += HandleCollectButtonClicked;
        }

        public void Exit()
        {
            _view.OnCloseButtonClicked -= HandleCloseButtonClicked;
            _view.OnCollectButtonClicked -= HandleCollectButtonClicked;
            _saver.SaveScore(_score.CurrentScore);
        }

        private void HandleCloseButtonClicked() => _uiSwitcher.SwitchState(UIState.MainScreen);

        private void HandleCollectButtonClicked()
        {
            int newScore = _score.AddScore();
            _view.UpdateScore(newScore);
        }
    }
}