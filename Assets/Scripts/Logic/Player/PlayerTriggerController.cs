using UnityEngine;

namespace Logic.Player
{
    public class PlayerTriggerController: MonoBehaviour
    {
        private const string ExitTag = "Exit";
        private const string EnemyTag = "Enemy";

        private void OnTriggerEnter(Collider other)
        {
            switch (other.tag)
            {
                case ExitTag:
                    EventManager.OnEndGame.Invoke(new EndGameStatus(true));
                    break;
                case EnemyTag:
                    EventManager.OnEndGame.Invoke(new EndGameStatus(false));
                    break;
            }
        }
    }
}