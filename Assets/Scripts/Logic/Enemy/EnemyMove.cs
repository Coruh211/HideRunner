using System.Collections.Generic;
using Infrastructure.Services;
using Logic.Generator;
using Logic.Player;
using StaticData;
using UniRx;
using UnityEngine;
using UnityEngine.AI;


namespace Logic.Enemy
{
    public class EnemyMove: MonoBehaviour
    {
        [SerializeField] private NavMeshAgent Agent;
        [SerializeField] private int GoalsCount;
        [SerializeField] private float DistanceToChangeGoal;
        [SerializeField] private float OffsetToCenterGoal = 0.5f;
        [SerializeField] private AnimatorController AnimatorController;
        

        private ILevelConstructorService _levelConstructorService;
        private INoiseController _noiseController;
        private List<Transform> _goals = new List<Transform>();
        private EnemySO _enemySo;
        private int _currentGoal;
        private bool _startGame;
        private bool _startChasing;
        private GameObject _player;
        

        public void Init(ILevelConstructorService levelConstructorService, EnemySO enemySo)
        {
            _levelConstructorService = levelConstructorService;
            _enemySo = enemySo;
            _noiseController = AllServices.Container.Single<INoiseController>();
            _player = GameObject.FindGameObjectWithTag("Player");
            _noiseController.MaxNoise += StartChasing;
            EventManager.OnStartGame.Subscribe(StartMove);
        }
        
        public void StartChasing() => 
            _startChasing = true;

        private void StartMove()
        {
            GetGoals();
            
            Agent.destination = _goals[_currentGoal].position;
            AnimatorController.SetState(1);
            Agent.speed = _enemySo.Speed;
            _startGame = true;
        }
        
        private void Update()
        {
            if (!_startGame)
            {
                return;
            }

            if (_startChasing)
            {
                Agent.destination = _player.transform.position;
                return;
            }
            
            if (Agent.remainingDistance < DistanceToChangeGoal)
            {
                _currentGoal++;
                
                if (_currentGoal == _goals.Count) 
                    _currentGoal = 0;
                
                Agent.destination = _goals[_currentGoal].position;
            }
        }

        private void GetGoals()
        {
            for (int i = 0; i < GoalsCount; i++)
            {
                var floorList = _levelConstructorService.GetFloorTransform();
                var randomGoal = floorList[Random.Range(0, floorList.Count)];
                var position = randomGoal.position;
                position.ChangeX(position.x + OffsetToCenterGoal);
                position.ChangeZ(position.z + OffsetToCenterGoal);
                _goals.Add(randomGoal);
            }
        }

       
    }
}