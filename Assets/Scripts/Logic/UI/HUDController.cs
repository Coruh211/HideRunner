using UnityEngine;

namespace Logic.UI
{
    public class HUDController : MonoBehaviour
    {
        [SerializeField] private GameObject StartWindow;
        [SerializeField] private GameObject GameWindow;
        [SerializeField] private GameObject EndGameWindow;

        public void ShowStartWindow()
        {
            GameWindow.SetActive(false);
            EndGameWindow.SetActive(false);
            StartWindow.SetActive(true);
        }
        
        private void Start()
        {
            GameWindow.SetActive(false);
            EndGameWindow.SetActive(false);
            EventManager.OnStartGame.Subscribe(ShowGameWindow);
            EventManager.OnEndGame.Subscribe(ShowEndWindow);
        }

        private void ShowEndWindow(EndGameStatus status)
        {
            EndGameWindow.SetActive(true);
            EndGameWindow.GetComponent<EndGameWindowPresenter>().ChangeText(status.win);
            GameWindow.SetActive(false);
            StartWindow.SetActive(false);
        }

        private void ShowGameWindow()
        {
            GameWindow.SetActive(true);
            EndGameWindow.SetActive(false);
            StartWindow.SetActive(false);
        }
    }
}