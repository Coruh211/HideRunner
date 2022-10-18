using Infrastructure.Services;
using Logic.Generator;
using TMPro;
using UnityEngine;

namespace Logic.UI
{
    public class EndGameWindowPresenter: MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI EndText;
        
        public void ClickNextButton()
        {
            AllServices.Container.Single<ILevelControllerService>().ConstructNewLevel();
        }

        public void ChangeText(bool winCondition)
        {
            if (winCondition)
            {
                EndText.text = "You Win!";
            }
            else
            {
                EndText.text = "You lose(";
            }
        }
        
        
    }
}