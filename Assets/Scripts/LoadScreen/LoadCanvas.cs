using System;
using Infrastructure;
using UniRx;
using UnityEngine;

namespace LoadScreen
{
    public class LoadCanvas: MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        public void Show() => 
            gameObject.SetActive(true);

        public void Hide() => 
            gameObject.SetActive(false);
    }
}