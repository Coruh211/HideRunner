using System;
using System.Collections.Generic;
using Infrastructure.Services;
using UnityEngine;

namespace Logic.Logic.UI
{
    public class HUDPresenter : MonoBehaviour
    {
        [SerializeField] private List<InputFields> inputFieldsList;

        public List<InputFields> GetInputFieldsList => 
            inputFieldsList;

        private void Start()
        {
            AllServices.Container.Single<IUISelector>().SelectAllInputFields += HideHUD;
        }

        private void HideHUD() => 
            gameObject.SetActive(false);
    }
}