using System;

namespace Logic.Input
{
    public class GlobalInputState: Singleton<GlobalInputState>
    {
        public event Action StartGameAction;
        public event Action EndGameAction; 
        public bool InputEnable = false;
        
        public void ChangeInputState()
        {
            InputEnable = !InputEnable;
        }

        private void Start()
        {
            EventManager.OnStartGame.Subscribe(ChangeInputState);
            EventManager.OnStartGame.Subscribe(StartGame);
            EventManager.OnEndGame.Subscribe(EndGame);
        }
        
        private void StartGame()
        {
            StartGameAction?.Invoke();
        }

        private void EndGame(EndGameStatus obj)
        {
            EndGameAction?.Invoke();
        }
    }
}