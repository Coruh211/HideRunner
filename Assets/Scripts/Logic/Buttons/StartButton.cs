using System;
using UnityEngine;
using UnityEngine.UI;

namespace Logic.Buttons
{
    public class StartButton: MonoBehaviour
    {
        [SerializeField] private Button _startButton;

        private void Start()
        {
            _startButton.onClick.AddListener(StartGame);
        }

        private void StartGame()
        {
            EventManager.OnStartGame.Invoke();
            gameObject.SetActive(false);
        }
    }
}