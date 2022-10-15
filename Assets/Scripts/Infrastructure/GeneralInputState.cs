namespace Infrastructure
{
    public class GeneralInputState: Singleton<GeneralInputState>
    {
        public bool input;

        private void Start()
        {
            EventManager.OnStartGame.Subscribe(EnableInput);
        }

        public void EnableInput() => 
            input = true;

        public void DisableInput() => 
            input = false;
    }
}