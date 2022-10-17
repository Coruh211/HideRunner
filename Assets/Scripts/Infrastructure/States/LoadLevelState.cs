using Infrastructure.Factory;
using Infrastructure.Services;
using LoadScreen;
using Logic.Generator;
using Logic.NavMesh;
using Logic.Player;
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
            _allServices.RegisterSingle<ILevelConstructorService>(new LevelConstructor());
            _allServices.RegisterSingle<IGameConstructor>(new GameConstructor(_allServices.Single<ILevelConstructorService>()));
            _allServices.RegisterSingle<INoiseController>(new PlayerNoise());
        }

        private void OnLoaded()
        {
            _allServices.Single<ILevelConstructorService>().Construct();
            
            NavMeshBaker.Instance.BakeMesh();
            
            _allServices.Single<IGameConstructor>().Construct();
            
            _stateMachine.Enter<GameLoopState>();
        }
    }
}