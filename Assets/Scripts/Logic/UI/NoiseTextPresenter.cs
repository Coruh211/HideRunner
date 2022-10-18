using Infrastructure.Services;
using Logic.Player;
using TMPro;
using UnityEngine;

namespace Logic.UI
{
    public class NoiseTextPresenter: MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI TextMeshProUGUI;
        private INoiseController _noiseController;

        private void Start()
        {
            _noiseController = AllServices.Container.Single<INoiseController>();
            _noiseController.UpdateNoise += UpdateText;
        }

        private void UpdateText(float noiseLevel)
        {
            TextMeshProUGUI.text = "Noise: " + noiseLevel;
        }
    }
}