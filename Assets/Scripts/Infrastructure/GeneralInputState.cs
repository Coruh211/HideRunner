namespace Infrastructure
{
    public class GeneralInputState: Singleton<GeneralInputState>
    {
        public bool Input;

        private void Start()
        {
            EventManager.OnStartGame.Subscribe(EnableInput);
        }

        public void EnableInput() => 
            Input = true;

        public void DisableInput() => 
            Input = false;
    }
}