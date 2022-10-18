using System.Collections.Generic;
using Infrastructure.Services;
using Logic.Generator;
using Logic.Input;
using Logic.Player;
using StaticData;
using UnityEngine;
using UnityEngine.AI;


namespace Logic.Enemy
{
    public class EnemyMove: MonoBehaviour
    {
        [SerializeField] private AnimatorController AnimatorController;
        [SerializeField] private SkinnedMeshRenderer ModelMeshRenderer;
        [SerializeField] private Material AgreMaterial;
        
        
        private NavMeshAgent _agent;
        
        private ILevelConstructorService _levelConstructorService;
        private INoiseController _noiseController;
        private List<Transform> _goals;
        
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
            _noiseController.MaxNoise += StartChasing;

            GlobalInputState.Instance.StartGameAction += StartMove;
        }

        public void StartChasing() => 
            _startChasing = true;

        private void StartMove()
        {
            GlobalInputState.Instance.StartGameAction -= StartMove;
                
            _goals = new List<Transform>();
            GetGoals();
            
            _player = GameObject.FindGameObjectWithTag("Player");
            _agent = GetComponent<NavMeshAgent>();
            _agent.destination = _goals[_currentGoal].position;
            AnimatorController.SetState(1);
            _agent.speed = _enemySo.Speed;
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
                _agent.destination = _player.transform.position;
                ModelMeshRenderer.material = AgreMaterial;
                return;
            }
            
            if (_agent.remainingDistance < _enemySo.DistanceToChangeGoal)
            {
                _currentGoal++;
                
                if (_currentGoal == _goals.Count) 
                    _currentGoal = 0;
                
                _agent.destination = _goals[_currentGoal].position;
            }
        }

        private void GetGoals()
        {
            for (int i = 0; i < _enemySo.GoalsCount; i++)
            {
                var floorList = _levelConstructorService.GetFloorTransform();
                var randomGoal = floorList[Random.Range(0, floorList.Count)];
                var position = randomGoal.position;
                position.ChangeX(position.x + _enemySo.OffsetToCenterGoal);
                position.ChangeZ(position.z + _enemySo.OffsetToCenterGoal);
                _goals.Add(randomGoal);
            }
        }
    }
}