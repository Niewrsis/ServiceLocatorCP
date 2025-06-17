using Zenject;
using UnityEngine;
using UI;
using Services;
using UI.View;
using System.Collections.Generic;
using ScoreSystem;
using UI.Controllers;

public class Bootstrapper : MonoInstaller
{
    [Header("Prefabs")]
    [SerializeField] private MainScreenView _mainScreenViewPrefab;
    [SerializeField] private PanelView _panelViewPrefab;

    [Header("Audio")]
    [SerializeField] private AudioSource _audioSourcePrefab;
    [SerializeField] private AudioClip _openClip;
    [SerializeField] private AudioClip _closeClip;

    public override void InstallBindings()
    {
        Container.Bind<IFadeService>().To<FadeService>().AsSingle();
        Container.Bind<ISaver>().To<PlayerPrefsSaver>().AsSingle();

        Container.Bind<ISoundPlayer>().To<SoundPlayer>().AsSingle()
            .WithArguments(_audioSourcePrefab, _openClip, _closeClip);

        Container.Bind<Score>().AsSingle().NonLazy();

        Container.Bind<MainScreenView>()
            .FromComponentInNewPrefab(_mainScreenViewPrefab)
            .AsSingle()
            .NonLazy();

        Container.Bind<PanelView>()
            .FromComponentInNewPrefab(_panelViewPrefab)
            .AsSingle()
            .NonLazy();

        Container.BindInterfacesAndSelfTo<MainScreenController>().AsSingle();
        Container.BindInterfacesAndSelfTo<PanelController>().AsSingle();

        Container.Bind<UISwitcher>().FromNewComponentOnNewGameObject()
            .AsSingle()
            .WithArguments(new List<UIState> { UIState.MainScreen, UIState.Panel })
            .NonLazy();
    }
}