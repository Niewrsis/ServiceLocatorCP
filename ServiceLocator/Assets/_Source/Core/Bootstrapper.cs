using ScoreSystem;
using Services;
using System.Collections.Generic;
using UI;
using UI.Controllers;
using UI.View;
using UnityEngine;

namespace Core
{
    public class Bootstrapper : MonoBehaviour
    {
        [SerializeField] private MainScreenView _mainScreenView;
        [SerializeField] private PanelView _panelView;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _openClip;
        [SerializeField] private AudioClip _closeClip;

        private ServiceLocator _serviceLocator;
        private Score _score;
        private UISwitcher _uiSwitcher;

        void Awake()
        {
            _serviceLocator = new ServiceLocator();
            _score = new Score();
            _score.LoadFromPlayerPrefs();

            RegisterServices();
            CreateControllers();
            InitializeUISwitcher();
        }

        private void RegisterServices()
        {
            _serviceLocator.RegisterService<ISaver>(new PlayerPrefsSaver());

            // Применение с json:
            // _serviceLocator.RegisterService<ISaver>(new JsonSaver());

            _serviceLocator.RegisterService(new FadeService());
            _serviceLocator.RegisterService(new SoundPlayer(_audioSource, _openClip, _closeClip));
        }

        private void CreateControllers()
        {
            var mainScreenController = new MainScreenController(
                _mainScreenView,
                _uiSwitcher);

            var panelController = new PanelController(
                _panelView,
                _uiSwitcher,
                _score,
                _serviceLocator);

            _serviceLocator.RegisterService(mainScreenController);
            _serviceLocator.RegisterService(panelController);
        }

        private void InitializeUISwitcher()
        {
            _uiSwitcher = new UISwitcher();

            var states = new Dictionary<UIState, IUIState>
            {
                { UIState.MainScreen, _serviceLocator.GetService<MainScreenController>() },
                { UIState.Panel, _serviceLocator.GetService<PanelController>() }
            };

            _uiSwitcher.Initialize(
                states,
                _serviceLocator.GetService<IFadeService>(),
                _serviceLocator.GetService<ISoundPlayer>(),
                _panelView);
        }
    }
}