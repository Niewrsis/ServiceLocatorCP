using UnityEngine;
using System.Collections.Generic;
using Services;
using UI.View;

namespace UI
{
    public class UISwitcher : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float _fadeDuration = 0.3f;

        private IUIState _currentState;
        private Dictionary<UIState, IUIState> _states;
        private IFadeService _fadeService;
        private ISoundPlayer _soundPlayer;
        private PanelView _panelView;

        public void Initialize(
            Dictionary<UIState, IUIState> states,
            IFadeService fadeService,
            ISoundPlayer soundPlayer,
            PanelView panelView)
        {
            _states = states;
            _fadeService = fadeService;
            _soundPlayer = soundPlayer;
            _panelView = panelView;
        }

        public void SwitchState(UIState newState)
        {
            _currentState?.Exit();

            _currentState = _states[newState];
            _currentState.Enter();

            HandleStateEffects(newState);
        }

        private void HandleStateEffects(UIState state)
        {
            switch (state)
            {
                case UIState.Panel:
                    _fadeService.FadeIn(_panelView.CanvasGroup, _fadeDuration);
                    _soundPlayer.PlayOpenSound();
                    break;

                case UIState.MainScreen:
                    _fadeService.FadeOut(_panelView.CanvasGroup, _fadeDuration);
                    _soundPlayer.PlayCloseSound();
                    break;
            }
        }

        private void Start()
        {
            SwitchState(UIState.MainScreen);
        }
    }
}