using System;
using UnityEngine;
using UnityEngine.UI;

namespace Logic.Buttons
{
    public class StartButton: MonoBehaviour
    {
        public void StartGame() => 
            EventManager.OnStartGame.Invoke();
    }
}