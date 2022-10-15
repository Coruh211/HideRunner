using Infrastructure.Factory;
using Infrastructure.Services;
using StaticData;

namespace Infrastructure.States
{
    public class GameLoopState : IState
    {
        private readonly AllServices _allServices;
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly IGameFactory _gameFactory;

        public GameLoopState(GameStateMachine gameStateMachine, AllServices allServices, SceneLoader sceneLoader, IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
            _allServices = allServices;
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
        }

        private void EndGame()
        {
        }

        public void Exit()
        {
            
        }
        
        public void Enter()
        {
            
        }
    }
}