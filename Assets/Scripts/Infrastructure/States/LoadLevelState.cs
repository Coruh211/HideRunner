using Infrastructure.Factory;
using Infrastructure.Services;
using LoadScreen;
using Logic.Logic.Cube;
using Logic.Logic.UI;
using StaticData;
using UnityEngine;

namespace Infrastructure.States
{
    public class LoadLevelState: IPayloadedState<string>
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadCanvas _loadCanvas;
        private readonly IGameFactory _gameFactory;
        private readonly AllServices _allServices;
        private GameObject[] spawnPoints;
        private GameObject spawnContainer;
        private object uiSelector;


        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadCanvas loadCanvas,
            IGameFactory gameFactory, AllServices allServices)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _loadCanvas = loadCanvas;
            _gameFactory = gameFactory;
            _allServices = allServices;
        }

        public void Enter(string sceneName)
        {
            _loadCanvas.Show();
            RegisterServices();
            _sceneLoader.Load(sceneName, OnLoaded);
        }
        
        public void Exit()
        {
            _loadCanvas.Hide();    
        }

        private void RegisterServices()
        {
            _allServices.RegisterSingle<IUISelector>(new UISelector());
            _allServices.RegisterSingle<ICubeSpawner>(new CubeSpawner(_allServices.Single<IUISelector>()));
        }

        private void OnLoaded()
        {
            var hud = _gameFactory.CreateObject(AssetPath.HUDPath);
            var inputFieldsList = hud.GetComponent<HUDPresenter>().GetInputFieldsList;
            var selector = _allServices.Single<IUISelector>();
            selector.Init(inputFieldsList);

            _stateMachine.Enter<GameLoopState>();
        }
    }
}