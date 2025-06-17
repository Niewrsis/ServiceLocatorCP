using Services;
using System.Collections.Generic;
using UI.View;
using UnityEngine;
using Zenject;
using UI.Controllers;

namespace UI
{
    public class UISwitcher : MonoBehaviour
    {
        [SerializeField] private float _fadeDuration = 0.3f;

        private IUIState _currentState;
        private Dictionary<UIState, IUIState> _states;
        private IFadeService _fadeService;
        private ISoundPlayer _soundPlayer;
        private PanelView _panelView;

        [Inject]
        public void Construct(
            List<IUIState> states,
            IFadeService fadeService,
            ISoundPlayer soundPlayer,
            PanelView panelView)
        {
            _states = new Dictionary<UIState, IUIState>();
            foreach (var state in states)
            {
                if (state is MainScreenController)
                    _states.Add(UIState.MainScreen, state);
                else if (state is PanelController)
                    _states.Add(UIState.Panel, state);
            }

            _fadeService = fadeService;
            _soundPlayer = soundPlayer;
            _panelView = panelView;
        }

        private void Start() => SwitchState(UIState.MainScreen);

        public void SwitchState(UIState newState)
        {
            _currentState?.Exit();
            _currentState = _states[newState];
            _currentState.Enter();

            if (newState == UIState.Panel)
            {
                _fadeService.FadeIn(_panelView.CanvasGroup, _fadeDuration);
                _soundPlayer.PlayOpenSound();
            }
            else
            {
                _fadeService.FadeOut(_panelView.CanvasGroup, _fadeDuration);
                _soundPlayer.PlayCloseSound();
            }
        }
    }
}