using UI.View;
using Services;
using UnityEngine;
using ScoreSystem;

namespace UI.Controllers
{
    public class PanelController : IUIState
    {
        private readonly PanelView _view;
        private readonly UISwitcher _uiSwitcher;
        private readonly Score _score;
        private readonly ISaver _saver;

        public PanelController(PanelView view, UISwitcher uiSwitcher, Score score, IServiceLocator services)
        {
            _view = view;
            _uiSwitcher = uiSwitcher;
            _score = score;
            _saver = services.GetService<ISaver>();
        }

        public void Enter()
        {
            _view.UpdateScore(_score.CurrentScore);
            _view.OnCloseButtonClicked += HandleCloseButtonClicked;
            _view.OnCollectButtonClicked += HandleCollectButtonClicked;
            _view.gameObject.SetActive(true);
        }

        public void Exit()
        {
            _view.OnCloseButtonClicked -= HandleCloseButtonClicked;
            _view.OnCollectButtonClicked -= HandleCollectButtonClicked;
            _view.gameObject.SetActive(false);

            // Сохраняем при закрытии
            _saver.SaveScore(_score.CurrentScore);
        }

        private void HandleCloseButtonClicked()
        {
            _uiSwitcher.SwitchState(UIState.MainScreen);
        }

        private void HandleCollectButtonClicked()
        {
            _score.AddScore();
            _view.UpdateScore(_score.CurrentScore);
        }
    }
}