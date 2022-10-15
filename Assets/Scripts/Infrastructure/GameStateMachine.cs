
using System;
using System.Collections.Generic;
using Infrastructure.AssetManagement;
using Infrastructure.Factory;
using Infrastructure.Services;
using Infrastructure.States;
using LoadScreen;

namespace Infrastructure
{
    public class GameStateMachine
    {
        private readonly Dictionary<Type, IExsitableState> states;
        private IExsitableState activeState;

        public GameStateMachine(SceneLoader sceneLoader, LoadCanvas loadCanvas, AllServices services)
        {
            states = new Dictionary<Type, IExsitableState>()
            {
                [typeof (BootstrapState)] = new BootstrapState(this, sceneLoader, services),
                [typeof (LoadLevelState)] = new LoadLevelState(this, sceneLoader, loadCanvas, services.Single<IGameFactory>(), services),
                [typeof (GameLoopState)] = new GameLoopState(this, services, sceneLoader, services.Single<IGameFactory>())
            };
        }
        
        public void Enter<TState>() where TState : class, IState
        {
            activeState?.Exit();
            IState state = GetState<TState>() ;
            activeState = state;
            state.Enter();
        }
        
        public void Enter<TState, TPayload>(TPayload payload) where TState : class,  IPayloadedState<TPayload>
        {
            activeState?.Exit();
            IPayloadedState<TPayload> state = GetState<TState>();
            activeState = state;
            state.Enter(payload);
        }

        private TState GetState<TState>() where TState : class, IExsitableState =>  
            states[typeof(TState)] as TState;
        
    }
}